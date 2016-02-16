using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPoint.Services.Api.Models
{

    public static class Extensions
    {
        public static ItemInfo GetInfo(this List<ItemInfo> info, string key)
        {
            return info.FirstOrDefault(x => x.Name.ToLower() == key.ToLower());
        }

        public static int? GetInt(this List<ItemInfo> info, string key)
        {
            var prop = info.GetInfo(key);
            if (prop == null)
                return null;
            int n = 0;
            if (Int32.TryParse(prop.Value, out n))
            {
                return n;
            }
            return null;
        }
        public static bool? GetBool(this List<ItemInfo> info, string key)
        {
            var prop = info.GetInfo(key);
            if (prop == null)
                return null;
            bool b = false;
            if (Boolean.TryParse(prop.Value, out b))
            {
                return b;
            }
            return null;
        }

        public static string GetValue(this List<ItemInfo> info, string key)
        {
            var prop = info.GetInfo(key);
            if (prop == null)
                return "";
            return prop.Value;
        }

    }

    public class IntegratedSurveyModel
    {
        public string SurveyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool CanEvaluate { get; set; }
        public DateTime EvalOpen { get; set; }
        public DateTime EvalClose { get; set; }
        public int MinutesAfterSessionToOpen { get; set; }
        public List<QuestionGroup> QuestionGroups { get; set; }

        public class QuestionGroup
        {
            public string Name { get; set; }
            public string HeaderText { get; set; }
            public int Order { get; set; }
            public List<Question> Questions { get; set; }

            public class Question
            {
                public string Id { get; set; }
                public string QNum { get; set; }
                public string QuestionType { get; set; }
                public string Text { get; set; }
                public string Label { get; set; }
                public int Order { get; set; }
                public bool Required { get; set; }
                public int MinNumAnswers { get; set; }
                public int MaxNumAnswers { get; set; }
                public string MinNumAnswersAlert { get; set; }
                public string MaxNumAnswersAlert { get; set; }
                public int MaxLength { get; set; }
                public List<Answer> Answers { get; set; }
                public List<Rule> Rules { get; set; }

                public class Answer
                {
                    public string Id { get; set; }
                    public int Num { get; set; }
                    public string Text { get; set; }
                    public string Label { get; set; }
                }

                public class Rule
                {
                    public string Operand { get; set; }
                    public string Comparison { get; set; }
                    public string Value { get; set; }
                    public string Action { get; set; }
                }
            }
        }
    }

    #region API MODELS OF RELEVANCE

    public class AuthTokenResult
    {
        public string AuthToken { get; set; }
        public string RegistrantKey { get; set; }
        public string IPID { get; set; }
        public string Dest { get; set; }
        public string Url { get; set; }
        public string IPEmailAddress { get; set; }
    }

    public class Program
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimezoneOffset { get; set; }
        public string TimezoneId { get; set; }
        public List<ProgramDay> Days { get; set; }
        public List<Category> Categories { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Timeslot> Timeslots { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Agenda> Agenda { get; set; }
    }

    public class ProgramConfiguration
    {
        public bool CanEvaluateSessions { get; set; }
        public bool AllowDoubleBooking { get; set; }
        public bool FreeFormTimes { get; set; }
        public bool IncludeDocuments { get; set; }
        public bool SSL { get; set; }
        public int? CacheInterval { get; set; }
        public List<ItemInfo> Info { get; set; }
    }


    public class ProgramDay
    {
        public DateTime Date { get; set; }
        public string LongFormattedDate { get; set; }
        public string ShortFormattedDate { get; set; }
        public string DateString { get; set; }
        public int DateIndex { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public bool Publish { get; set; }
    }

    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Booth { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public List<string> CategoryIds { get; set; }
        public List<ItemInfo> Info { get; set; }
    }

    public class Timeslot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Type { get; set; }
        public string FormattedTimeslot { get; set; }
        public bool Publish { get; set; }
    }

    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public bool Publish { get; set; }
        public string Type { get; set; }
    }

    public class Agenda
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string FormattedTimeslot { get; set; }
        public string Free1 { get; set; }
        public string Free2 { get; set; }
        public string Free3 { get; set; }
        public string Free4 { get; set; }
        public List<ItemInfo> Info { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }

    public class Topic
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ApprovalStatus { get; set; }
        public string PublishingStatus { get; set; }
        public string Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public double Score { get; set; }
        public List<string> CategoryIds { get; set; }
        public List<string> SpeakerIds { get; set; }
        public List<Session> Sessions { get; set; }
        public List<RelatedInfo> Related { get; set; }
        public List<ItemInfo> Info { get; set; }
        public List<Document> Documents { get; set; }
        public List<SpeakerItem> SpeakerTypes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int TN { get; set; }
    }


    public class TopicData
    {
        public string Id { get; set; }
        public string RegistrantKey { get; set; }
        public Topic Topic { get; set; }
        public List<ItemInfo> Info = new List<ItemInfo>();
        public List<ItemCategory> Categories = new List<ItemCategory>();
        public List<ItemSpeaker> Speakers = new List<ItemSpeaker>();
    }


    public class Report
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.Data.DataSet Data { get; set; }
    }
    public class ReportData
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
    }

    public class Document
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
        public string ObjectId { get; set; }
        public string DocumentType { get; set; }
    }

    public class Rating
    {
        public double? Score { get; set; }
        public string Comment { get; set; }
        public Registrant Registrant { get; set; }
        public DateTime? Created { get; set; }
    }

    public class ItemInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class RelatedTopic
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class RelatedInfo
    {
        public string Name { get; set; }
        public string[] Values { get; set; }
        public List<RelatedTopic> Topics { get; set; }
    }

    public class Session
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string TimeslotId { get; set; }
        public bool AllowScheduling { get; set; }
        public int Capacity { get; set; }
    }

    public class Speaker
    {
        public string Id { get; set; }
        public string RegistrantKey { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
        public string Bio { get; set; }
        public List<ItemInfo> Info { get; set; }
        public List<string> CategoryIds { get; set; }
        public List<string> TopicIds { get; set; }
    }


    public class SessionChangeNotification
    {
        public DateTime ChangeDate { get; set; }
        public string ChangeType { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Summary { get; set; }
        public Topic Topic { get; set; }
    }

    public class RegistrantResult
    {
        public Registrant Registrant { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }


    public class LocationInfo
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }


    public class ItemCategory
    {
        public string ParentID { get; set; }
        public string[] CategoryIds { get; set; }
    }

    public class ItemSpeaker
    {
        public string SpeakerType { get; set; }
        public string[] Registrants { get; set; }
    }

    public class SpeakerItem
    {
        public string SpeakerType { get; set; }
        public string SpeakerId { get; set; }
        public string RegistrantKey { get; set; }
    }


    public class Registrant
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
        public string QLKey { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }
        public string ThirdPartyId { get; set; }
        public string ThirdPartyCode { get; set; }
        public DateTime? RegistrationCompleteDate { get; set; }
        public bool ShareSchedule { get; set; }
        public bool ShareContacts { get; set; }
        public bool ShareTripReport { get; set; }
        public bool DirectoryOptIn { get; set; }
        public LocationInfo Location { get; set; }
        public List<ItemInfo> Info { get; set; }
        public List<string> CategoryIds { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string CID { get; set; }
    }

    public class RegistrantEvalResult
    {
        public string RegistrantKey { get; set; }
        public string SurveyId { get; set; }
        public string SurveyResponseId { get; set; }
        public string SurveyResultId { get; set; }
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public DateTime Created { get; set; }
    }

    public class RegistrantInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class RegistrantCategory
    {
        public string ParentID { get; set; }
        public string[] CategoryIds { get; set; }
    }

    public class RegistrantData
    {
        public string Key { get; set; }
        public Registrant Registrant { get; set; }
        public List<RegistrantInfo> Info = new List<RegistrantInfo>();
        public List<RegistrantCategory> Categories = new List<RegistrantCategory>();
    }


    public class RegistrantSchedule
    {
        public List<Topic> Topics { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Meeting> Meetings { get; set; }
    }

    public class SessionMonitoringRecord
    {
        public string RegUser { get; set; }
        public string SubmittedRegId { get; set; }
        public DateTime ScanTime { get; set; }
        public string Created { get; set; }
        public string TopicID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string SessionID { get; set; }
        public string TimeslotID { get; set; }
        public string RoomID { get; set; }
        public int? Capacity { get; set; }
        public string RegistrantKey { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdPartyID { get; set; }
        public string Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime FinishUtc { get; set; }

    }


    #region meeting models

    public class Appointment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Location { get; set; }
        public string FormattedTimeslot { get; set; }
        public Registrant Organizer { get; set; }
    }

    public class Meeting
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Location { get; set; }
        public string LocationDescription { get; set; }
        public string FormattedTimeslot { get; set; }
        public Registrant Organizer { get; set; }
        public List<MeetingParticipant> Participants { get; set; }
    }

    public class MeetingParticipant
    {
        public Registrant Registrant { get; set; }
        public string Status { get; set; }
    }


    #endregion


    #region discussion models

    public class Thread
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string ObjectID { get; set; }
        public string ObjectType { get; set; }
        public string Body { get; set; }
        public int NumPosts { get; set; }
        public string LastMessageId { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? LastPostDate { get; set; }
        public Registrant LastAuthor { get; set; }
        public DateTime Created { get; set; }
        public Registrant Author { get; set; }
        public List<Message> Replies { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ThreadId { get; set; }
        public string ParentId { get; set; }
        public DateTime Created { get; set; }
        public int Level { get; set; }
        public Registrant Author { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public string MessageType { get; set; }
    }

    public class UserGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLocked { get; set; }
        public Registrant Owner { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Registrant> Members { get; set; }
    }
    #endregion


    #region eval models

    public class Survey
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public List<string> CategoryIds { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class SurveyObject
    {
        public string SurveyId { get; set; }
        public string ObjectId { get; set; }
        public DateTime CompletedDate { get; set; }
    }

    public class SurveyResponseList
    {
        public string Message { get; set; }
        public List<SurveyObject> Surveys { get; set; }
    }

    public class Question
    {
        public string Id { get; set; }
        public int Num { get; set; }
        public string QuestionType { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class SurveyResult
    {
        public string SurveyId { get; set; }
        public List<AnsweredQuestion> AnsweredQuestions { get; set; }
    }

    public class AnsweredQuestion
    {
        public string QuestionId { get; set; }
        public string[] AnswerIds { get; set; }
        public string ResponseText { get; set; }
    }

    public class Answer
    {
        public string Id { get; set; }
        public int Num { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }
    }


    #endregion


    #region webmodels

    public class Announcement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public int? Num { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? UnpublishDate { get; set; }
    }


    public class Content
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public string Url { get; set; }
    }

    public class Page
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsOffline { get; set; }
        public bool IsPublic { get; set; }
        public bool IsMobile { get; set; }
        public bool EnforceSecurity { get; set; }
        public string Keywords { get; set; }
        public Content PublishedContent { get; set; }
        public List<ItemInfo> Configuration { get; set; }
    }

    public class Navigation
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Attributes { get; set; }
        public string Target { get; set; }
        public int NavOrder { get; set; }
        public int Level { get; set; }
    }

    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public List<Navigation> Navigation { get; set; }
    }

    #endregion


    #region transport models

    public class PagedResult<T>
    {
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public List<T> Results { get; set; }
    }


    public class ServiceResult
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class MeetingValidationResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool CanSchedule { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool LocationAvailable { get; set; }
        public bool TimeAvailable { get; set; }
    }

    #endregion


    #endregion


}
