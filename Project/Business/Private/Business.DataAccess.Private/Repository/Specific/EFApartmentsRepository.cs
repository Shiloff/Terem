using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;

namespace Business.DataAccess.Private.Repository.Specific
{
    public class EFApartmentsRepository : IApartmentRepository
    {
        EFDBContext context = new EFDBContext();

        public IQueryable<Apartment> Apartments
        {
            get
            {
                return context.Apartments;
            }
        }
        public IQueryable<ApartmentType> ApartmentTypes
        {
            get
            {
                return context.ApartmentTypes;
            }
        }
        public void AddApartment(Apartment apartment)
        {
            context.Apartments.Add(apartment);
            context.SaveChanges();
        }
        public Apartment GetApartment(long apartmentId)
        {
            return context.Apartments
                .Include(m => m.Type)
                .Include(m => m.Profile)
                .Include(m => m.ApartmentOptions)
                .Include(m => m.ApartmentComments.Select(k => k.ApartmentCommentsLikes))
                .Include(m => m.ApartmentComments.Select(k => k.Profile))
                .Include(m => m.ApartmentPhotos.Select(k => k.Links))
                .Include(m => m.ApartmentVisitors.Select(k => k.Profile))
                .Where(m => m.ApartmentId == apartmentId)
                .FirstOrDefault();
        }
        public void RemoveApartment(long apartmentId)
        {
            throw new NotImplementedException();
        }
        public void UpdateApartment(Apartment apartment)
        {
            context.Apartments.AddOrUpdate(e => e.ApartmentId, apartment);
            context.SaveChanges();
        }
        private IQueryable<Apartment> GetApartmentQuery(GetApartmentsParams param)
        {           
            return context.Apartments.Where(m => m.Published == true)
                .Include(m => m.Profile)
                .Include(m => m.ApartmentOptions)
                .Include(m => m.Type)
                .Include(m => m.DefaultPhoto.Links)
                .Include(m => m.ApartmentPhotos.Select(k => k.Links))
                .OrderByDescending(m => m.UpdateDate)
                .AsQueryable();
        }
        public List<Apartment> GetApartments(GetApartmentsParams param)
        {
            var query = GetApartmentQuery(param);
            if (param.Take != 0)
            {
                query = query.Skip(param.Skip).Take(param.Take);
            }
            return query               
                .ToList();
        }
        public long GetApartmentsCount(GetApartmentsParams param)
        {
            var query = GetApartmentQuery(param);
            return query.Count();
        }

        public List<Apartment> GetMyApartments(long profileId)
        {
            return Apartments.Where(m => m.ProfileId == profileId)
                .Include(m => m.Type)
                .Include(m => m.ApartmentOptions)
                .Include(m => m.ApartmentPhotos.Select(k => k.Links))
                .ToList();
        }
        public List<ApartmentOption> GetApartmentOptions()
        {
            return context.ApartmentOptions.ToList();
        }

        void RemoveOption(Apartment apartment, ApartmentOption option)
        {
            ApartmentOption Option = context.ApartmentOptions.Find(option.ApartmentOptionId);
            Apartment Apartment = context.Apartments.Include(m => m.ApartmentOptions).Where(m => m.ApartmentId == apartment.ApartmentId).FirstOrDefault();

            if ((Apartment != null) && (Option != null) && (Apartment.ApartmentOptions.Contains(Option)))
            {
                Apartment.ApartmentOptions.Remove(Option);
                //context.SaveChanges();
            }
        }
        public void AddOption(Apartment aparment, ApartmentOption option)
        {
            ApartmentOption Option = context.ApartmentOptions.Find(option.ApartmentOptionId);
            Apartment Apartment = context.Apartments.Include(m => m.ApartmentOptions).Where(m => m.ApartmentId == aparment.ApartmentId).FirstOrDefault();
            if ((Apartment != null) && (Option != null) && (!Apartment.ApartmentOptions.Contains(Option)))
            {

                Apartment.ApartmentOptions.Add(Option);
                context.SaveChanges();
            }
        }
        public void ClearApartmentOptions(Apartment apartment)
        {
            Apartment clearApartment = context.Apartments.Include(m => m.ApartmentOptions).Where(m => m.ApartmentId == apartment.ApartmentId).FirstOrDefault();
            List<ApartmentOption> AllOptions = clearApartment.ApartmentOptions.ToList();
            foreach (ApartmentOption option in AllOptions)
            {
                RemoveOption(clearApartment, option);
            }
            context.SaveChanges();
        }
        public List<ApartmentPhoto> GetApartmentPhotos(Apartment apartment)
        {
            List<ApartmentPhoto> result = context
                .Apartments
                .Where(m => m.ApartmentId == apartment.ApartmentId)
                .SelectMany(m => m.ApartmentPhotos).ToList();
            return result;
        }
        public void AddApartmentPhoto(Apartment apartment, ApartmentPhoto photo)
        {
            Apartment Apartment = context
                .Apartments
                .Include(m => m.ApartmentPhotos)
                .Include(m => m.DefaultPhoto)
                .Where(m => m.ApartmentId == apartment.ApartmentId)
                .FirstOrDefault();
            Apartment.ApartmentPhotos.Add(photo);

            context.SaveChanges();
            if (Apartment.DefaultPhoto == null)
            {
                SetApartmentDefaultPhoto(Apartment, photo);
            }
        }
        public void SetApartmentDefaultPhoto(Apartment apartment, ApartmentPhoto photo)
        {
            Apartment Apartment = context
                .Apartments
                .Include(m => m.ApartmentPhotos)
                .Where(m => m.ApartmentId == apartment.ApartmentId)
                .FirstOrDefault();
            Apartment.DefaultPhoto = photo;
            context.SaveChanges();
        }
        public void RemoveApartmentPhoto(long? ApartmentPhotoId)
        {
            List<ApartmentPhotoLink> photoToDelete = context.ApartmentPhotosLinks.Where(m => m.ApartmentPhotoId == ApartmentPhotoId).ToList();
            foreach (var photo in photoToDelete)
            {
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath(photo.Link));
                context.ApartmentPhotosLinks.Remove(photo);
            }
            ApartmentPhoto Photo = context.ApartmentPhotos.Find(ApartmentPhotoId);
            context.ApartmentPhotos.Remove(Photo);
            context.SaveChanges();
        }

        public void UpdateCoords(Apartment apartment, ApartmentCoords coords)
        {
            Apartment Apartment = context
                .Apartments
                .Where(m => m.ApartmentId == apartment.ApartmentId).FirstOrDefault();
            if (Apartment != null)
            {
                Apartment.Latitude = coords.Latitude;
                Apartment.Longitude = coords.Longitude;
                context.SaveChanges();
            }
        }

        public void SetPublished(long apartmentId)
        {
            Apartment Apartment = context
               .Apartments
               .Where(m => m.ApartmentId == apartmentId).FirstOrDefault();
            Apartment.Published = true;
            Apartment.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }
        public void RemovePublished(long apartmentId)
        {
            Apartment Apartment = context
                 .Apartments
                 .Where(m => m.ApartmentId == apartmentId).FirstOrDefault();
            Apartment.Published = false;
            Apartment.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }
        public void AddProfileVisit(long apartmentId, long profileId)
        {
            ApartmentVisitors visit = context.ApartmentVisitors
                .Where(m => m.ProfileId == profileId && m.ApartmentId == apartmentId)
                .FirstOrDefault();
            if (visit == null)
            {
                visit = new ApartmentVisitors { ProfileId = profileId, ApartmentId = apartmentId, Count = 0 };
                context.ApartmentVisitors.Add(visit);
            }
            visit.LastDate = DateTime.Now;
            visit.Count++;
            context.SaveChanges();
        }
        public List<Profile> GetProfileVisit(long apartmentId)
        {
            List<Profile> result;
            result = context.ApartmentVisitors
                .Where(m => m.ApartmentId == apartmentId)
                .Select(m => m.Profile)                
                .ToList();
            //.Include(m => m.ApartmentVisitors.Select(k => k.Profile))
            return result;
        }
        public bool IsMyApartment(long apartmentId, long profileId)
        {
            bool result = false;
            var apartment = context
                .Apartments
                .Where(m => m.ApartmentId == apartmentId && m.ProfileId == profileId)
                .FirstOrDefault();
            if (apartment != null)
            {
                result = true;
            }
            return result;
        }
        public bool IsApartmentExists(long apartmentId)
        {
            bool result = false;
            var apartment = context
                .Apartments
                .Where(m => m.ApartmentId == apartmentId)
                .FirstOrDefault();
            if (apartment !=null)
            {
                result = true;
            }
            return result;
        }


        #region ApartmentComment
        public List<ApartmentComment> GetApartmentComments(long apartmentId)
        {
            throw new NotImplementedException();
        }
        public ApartmentComment GetApartmentComment(long apartmentCommentId)
        {
            return context.ApartmentComments.Where(m => m.ApartmentCommentId == apartmentCommentId)
                .Include(m => m.Profile)
                .Include(m => m.ApartmentCommentsLikes)
                .FirstOrDefault();
        }
        public void AddApartmentComment(ApartmentComment apartmentComment)
        {
            context.ApartmentComments.Add(apartmentComment);
            context.SaveChanges();
        }
        public void RemoveApartmentComment(long apartmentCommentId)
        {
            ApartmentComment comment = context.ApartmentComments.Find(apartmentCommentId);
            if (comment != null)
            {
                context.ApartmentComments.Remove(comment);
                context.SaveChanges();
            }
        }
        public void AddApartmentCommentLike(long apartmentCommentId, long profileId)
        {
            ApartmentCommentLike like = context.ApartmentCommentLikes
                .Where(m => m.ApartmentCommentId == apartmentCommentId && m.ProfileId == profileId)
                .FirstOrDefault();
            if (like == null)
            {
                context.ApartmentCommentLikes.Add(new ApartmentCommentLike
                {
                    ApartmentCommentId = apartmentCommentId,
                    ProfileId = profileId,
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }
           
        }
        public void RemoveApartmentCommentLike(long apartmentCommentId, long profileId)
        {
            ApartmentCommentLike like = context.ApartmentCommentLikes
               .Where(m => m.ApartmentCommentId == apartmentCommentId && m.ProfileId == profileId)
               .FirstOrDefault();
            if (like != null)
            {
                context.ApartmentCommentLikes.Remove(like);
                context.SaveChanges();
            }
           
        }
        #endregion

    }

}
