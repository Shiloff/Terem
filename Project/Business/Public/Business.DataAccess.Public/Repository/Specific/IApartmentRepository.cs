using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific
{
    public struct GetApartmentsParams
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
    public interface IApartmentRepository
    {
        IQueryable<Apartment> Apartments { get; }
        IQueryable<ApartmentType> ApartmentTypes { get; }

        void AddApartment(Apartment apartment);
        Apartment GetApartment(long apartmentId);
        void RemoveApartment(long apartmentId);
        void UpdateApartment(Apartment apartment);
        List<Apartment> GetApartments(GetApartmentsParams param);
        long GetApartmentsCount(GetApartmentsParams param);
        List<Apartment> GetMyApartments(long profileId);
        List<ApartmentOption> GetApartmentOptions();
        //void RemoveOption(Apartment apartment, ApartmentOption option);
        void AddOption(Apartment aparment, ApartmentOption option);
        void ClearApartmentOptions(Apartment apartment);
        List<ApartmentPhoto> GetApartmentPhotos(Apartment apartment);
        void AddApartmentPhoto(Apartment apartment,ApartmentPhoto photo);
        void SetApartmentDefaultPhoto(Apartment apartment, ApartmentPhoto photo);
        void RemoveApartmentPhoto(long? ApartmentPhotoId);
        void UpdateCoords(Apartment apartment,ApartmentCoords coords);

        void SetPublished(long apartmentId);
        void RemovePublished(long apartmentId);
        void AddProfileVisit(long apartmentId, long profileId);
        List<Profile> GetProfileVisit(long apartmentId);
        bool IsMyApartment(long apartmentId, long profileId);
        bool IsApartmentExists(long apartmentId);
        #region ApartmentComment
        List<ApartmentComment> GetApartmentComments(long apartmentId);
        ApartmentComment GetApartmentComment(long apartmentCommentId);
        void AddApartmentComment(ApartmentComment apartmentComment);
        void RemoveApartmentComment(long apartmentCommentId);
        void AddApartmentCommentLike(long apartmentCommentId,long profileId);
        void RemoveApartmentCommentLike(long apartmentCommentId, long profileId);
        #endregion
    }
}
