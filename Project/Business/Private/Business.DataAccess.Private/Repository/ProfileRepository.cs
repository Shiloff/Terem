using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Contact;
using DataAccess.Private.Repository;
using IProfileRepository = Business.DataAccess.Public.Repository.IProfileRepository;

namespace Business.DataAccess.Private.Repository
{
    internal class ProfileRepository : RepositoryBase<Profile, long>, IProfileRepository
    {
        public ProfileRepository(DbContext context) : base(context)
        {
        }

        private EFDBContext DbContext => Context as EFDBContext;

        private IQueryable<Profile> DetailedProfiles
        {
            get
            {
                return DbContext.Profiles
                    .Include(m => m.Sex)
                    .Include(m => m.SexWho)
                    .Include(m => m.Alcohol)
                    .Include(m => m.Smoking)
                    .Include(m => m.Animals)
                    .Include(m => m.Intereses)
                    .Include(m => m.Activity);
            }
        }


        public Profile GetProfileWithDetails(long id)
        {
            return DetailedProfiles.FirstOrDefault(m => m.ProfileId == id);
        }

        public Profile GetShortProfile(long id)
        {
            return Get(id);
        }

        public Tuple<List<Profile>, int> GetContacts(long profileId, Pagination pagination, ContactFilter filter)
        {
            var query = DbContext.Profiles
                .Where(m => m.New == false)
                .Where(m => m.ProfileId != profileId)
                .Where(m => m.MyMessage.Any(k => k.ProfileIdTo == profileId)
                            || m.MessageForMe.Any(k => k.ProfileIdFrom == profileId))
                .Include(m => m.Intereses)
                .AsQueryable();
            query = ApplyFilters(query, filter);
            return new Tuple<List<Profile>, int>(ApplyPagination(query, pagination).ToList(), query.Count());
        }

        public Tuple<List<Profile>, int> FindContacts(long myProfileId, Pagination pagination, ContactFilter filter)
        {
            var query = DetailedProfiles
                .Where(m => m.ProfileId != myProfileId);
            query = ApplyFilters(query, filter);
            return new Tuple<List<Profile>, int>(ApplyPagination(query, pagination).ToList(), query.Count());
        }

        private static IQueryable<Profile> ApplyFilters(IQueryable<Profile> profiles, ContactFilter filter)
        {
            var customWhere = new CustomWhere<Profile>();

            if (filter.SexId != null)
            {
                customWhere.AddWhereClause(m => m.ProfileSexId == filter.SexId);
            }
            if (filter.SexWhoId != null)
            {
                customWhere.AddWhereClause(m => m.ProfileSexWhoId == filter.SexWhoId);
            }
            if (filter.AlcoholId != null)
            {
                customWhere.AddWhereClause(n => n.ProfileAlcoholId == filter.AlcoholId);
            }

            if (filter.ActivityId != null)
            {
                customWhere.AddWhereClause(m => m.ProfileActivityId == filter.ActivityId);
            }
            if (filter.AnimalId != null)
            {
                customWhere.AddWhereClause(m => m.ProfileAnimalsId == filter.AnimalId);
            }
            if (filter.SmokeId != null)
            {
                customWhere.AddWhereClause(m => m.ProfileSmokingId == filter.SmokeId);
            }

            return customWhere.Result != null ? profiles.Where(customWhere.Result) : profiles;
        }

        public class CustomWhere<T>
        {
            private ParameterExpression _param;
            private BinaryExpression _body;
            public CustomWhere()
            {
                _param = Expression.Parameter(typeof(T), "a");
                _body = null;
            }

            public void AddWhereClause(/*int? value, string propName,*/ Expression<Func<T, bool>> expr)
            {
                var binaryExpression = expr.Body as BinaryExpression;

                if (binaryExpression == null)
                {
                    throw new NotSupportedException($"{expr.Body.GetType()} not supported");
                }

                var visitor = new ParameterUpdateVisitor(expr.Parameters.First(), _param);
                // replace the parameter in the expression just created
                expr = visitor.Visit(expr) as Expression<Func<T, bool>>;

                _body = _body == null
                    ? expr.Body as BinaryExpression
                    : Expression.AndAlso(expr.Body, _body);
            }

            public Expression<Func<Profile, bool>> Result
                => _body == null ? null : Expression.Lambda<Func<Profile, bool>>(_body, _param);
        }

        private static IQueryable<Profile> ApplyPagination(IQueryable<Profile> profiles, Pagination filter)
        {
            profiles = profiles
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.ProfileId)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);
            return profiles;
        }
        public void Update(Profile profile, ProfileUpdateMode mode = ProfileUpdateMode.None)
        {
            switch (mode)
            {
                case ProfileUpdateMode.Main:
                    Update(profile.ProfileId,
                        () => new Profile
                        {
                            ProfileId = profile.ProfileId,
                            New = profile.New,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            Town = profile.Town,
                            ProfileSexId = profile.ProfileSexId,
                            ProfileActivityId = profile.ProfileActivityId,
                            Birfday = profile.Birfday,
                            ContactPhone = profile.ContactPhone
                        });
                    break;

                case ProfileUpdateMode.Dop:
                    Update(profile.ProfileId,
                        () => new Profile
                        {
                            ProfileId = profile.ProfileId,
                            New = profile.New,
                            AboutMe = profile.AboutMe,
                            ProfileAlcoholId = profile.ProfileAlcoholId,
                            ProfileSmokingId = profile.ProfileSmokingId,
                            ProfileAnimalsId = profile.ProfileAnimalsId,
                            ProfileSexWhoId = profile.ProfileSexWhoId
                        });
                    break;

                case ProfileUpdateMode.Avatar:
                    Update(profile.ProfileId,
                        () => new Profile
                        {
                            ProfileId = profile.ProfileId,
                            New = profile.New,
                            ImageType = profile.ImageType,
                            ImageLink = profile.ImageLink,
                            ImageAvatarType = profile.ImageAvatarType,
                            ImageAvatarLink = profile.ImageAvatarLink,
                            ImageAvatarBigType = profile.ImageAvatarBigType,
                            ImageAvatarBigLink = profile.ImageAvatarBigLink
                        });
                    break;
            }
        }

        public void UpdateIntereses(long profileId, int[] interesesId)
        {
            var profile = DbContext
                .Profiles
                .Include(m => m.Intereses)
                .Single(m => m.ProfileId == profileId);
            foreach (var interes in profile.Intereses.Select(m => m).ToList())
            {
                profile.Intereses.Remove(interes);
            }

            if (interesesId != null)
            {
                var newIntereses = DbContext.ProfileInteres.Where(m => interesesId.Contains(m.ProfileInteresId));
                foreach (var interes in newIntereses)
                {
                    profile.Intereses.Add(interes);
                }
            }
        }
    }

    class ParameterUpdateVisitor : ExpressionVisitor
    {
        private ParameterExpression _oldParameter;
        private ParameterExpression _newParameter;

        public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (object.ReferenceEquals(node, _oldParameter))
                return _newParameter;

            return base.VisitParameter(node);
        }
    }
}