using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Cart;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Certificate;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Feedback;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.StudentCourse;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Vouncher;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_API.AppStarts
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Account
            CreateMap<Account, AccountReponse>().ReverseMap();
            CreateMap<Account, CreateAccountRequest>().ReverseMap();
            CreateMap<Account, UpdateAccountRequest>().ReverseMap();
            CreateMap<AccountReponse, UpdateAccountRequest>().ReverseMap();
            CreateMap<AccountReponse, CreateAccountRequest>().ReverseMap();
            CreateMap<AccountReponse, AccountFilter>().ReverseMap();
            CreateMap<Account, CreateAccount1Request>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryReponse>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
            CreateMap<CategoryReponse, UpdateCategoryRequest>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<CategoryReponse, CategoryFilter>().ReverseMap();
            #endregion

            #region Certificate
            CreateMap<Certificate, CertificateReponse>().ReverseMap();
            CreateMap<Certificate, CreateCertificateRequest>().ReverseMap();
            CreateMap<Certificate, UpdateCertificateRequest>().ReverseMap();
            CreateMap<CertificateReponse, CreateCertificateRequest>().ReverseMap();
            CreateMap<CertificateReponse, UpdateCertificateRequest>().ReverseMap();
            CreateMap<CertificateReponse, CertificateFilter>().ReverseMap();
            #endregion

            #region Course
            CreateMap<Course, CourseReponse>().ReverseMap();
            CreateMap<Course, CreateCourseRequest>().ReverseMap();
            CreateMap<Course, UpdateCourseRequest>().ReverseMap();
            CreateMap<CourseReponse, CreateCourseRequest>().ReverseMap();
            CreateMap<CourseReponse, UpdateCourseRequest>().ReverseMap();
            CreateMap<CourseReponse, CourseFilter>().ReverseMap();
            #endregion

            #region Feedback
            CreateMap<Feedback, FeedbackReponse>().ReverseMap();
            CreateMap<Feedback, CreateFeedbackRequest>().ReverseMap();
            CreateMap<Feedback, UpdateFeedbackRequest>().ReverseMap();
            CreateMap<FeedbackReponse, UpdateFeedbackRequest>().ReverseMap();
            CreateMap<FeedbackReponse, CreateFeedbackRequest>().ReverseMap();
            #endregion

            #region Order
            CreateMap<Order, OrderReponse>().ReverseMap();
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<Order, UpdateOrderRequest>().ReverseMap();
            CreateMap<OrderReponse, UpdateOrderRequest>().ReverseMap();
            CreateMap<OrderReponse, CreateOrderRequest>().ReverseMap();
            CreateMap<OrderReponse, OrderFilter>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailOrderRequest>().ReverseMap();
            #endregion

            #region OrderDetail
            CreateMap<OrderDetail, OrderDetailReponse>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailRequest>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailRequest>().ReverseMap();
            CreateMap<OrderDetailReponse, UpdateOrderDetailRequest>().ReverseMap();
            CreateMap<OrderDetailReponse, CreateOrderDetailRequest>().ReverseMap();
            #endregion

            #region Slot
            CreateMap<Slot, SlotReponse>().ReverseMap();
            CreateMap<Slot, CreateSlotRequest>().ReverseMap();
            CreateMap<Slot, UpdateSlotRequest>().ReverseMap();
            CreateMap<SlotReponse, UpdateSlotRequest>().ReverseMap();
            CreateMap<SlotReponse, CreateSlotRequest>().ReverseMap();
            #endregion

            #region Student
            CreateMap<Student, StudentReponse>().ReverseMap();
            CreateMap<Student, CreateStudentCourseRequest>().ReverseMap();
            CreateMap<Student, UpdateStudentCourseRequest>().ReverseMap();
            CreateMap<StudentReponse, UpdateStudentCourseRequest>().ReverseMap();
            CreateMap<StudentReponse, CreateStudentCourseRequest>().ReverseMap();
            #endregion

            #region StudentCourse
            CreateMap<StudentCourse, StudentCourseReponse>().ReverseMap();
            CreateMap<StudentCourse, CreateStudentCourseRequest>().ReverseMap();
            CreateMap<StudentCourse, UpdateStudentCourseRequest>().ReverseMap();
            CreateMap<StudentCourseReponse, UpdateStudentCourseRequest>().ReverseMap();
            CreateMap<StudentCourseReponse, CreateStudentCourseRequest>().ReverseMap();
            #endregion

            #region Teacher
            CreateMap<Teacher, TeacherReponse>().ReverseMap();
            CreateMap<Teacher, CreateTeacherRequest>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherRequest>().ReverseMap();
            CreateMap<TeacherReponse, UpdateTeacherRequest>().ReverseMap();
            CreateMap<TeacherReponse, CreateTeacherRequest>().ReverseMap();
            #endregion

            #region Vouncher
            CreateMap<Vouncher, VouncherReponse>().ReverseMap();
            CreateMap<Vouncher, CreateVouncherRequest>().ReverseMap();
            CreateMap<Vouncher, UpdateVouncherRequest>().ReverseMap();
            CreateMap<VouncherReponse, UpdateVouncherRequest>().ReverseMap();
            CreateMap<VouncherReponse, CreateVouncherRequest>().ReverseMap();
            #endregion

            #region LiveChat
            CreateMap<LiveChatReponse, User>().ReverseMap();
            CreateMap<LiveChatRequest, User>().ReverseMap();
            CreateMap<LiveChatReponse, LiveChatFilter>().ReverseMap();

            CreateMap<LiveChatRequest, ChatMessageModel>()
                .ForMember(x => x.Message, opt => opt.MapFrom(dest => dest.Content))
                .ForMember(x => x.Receiver, opt => opt.MapFrom(dest => dest.ReceiverId))
                .ForMember(x => x.Sender, opt => opt.MapFrom(dest => dest.SenderId));

            CreateMap<ChatMessageModel, LiveChatRequest>()
                .ForMember(x => x.Content, opt => opt.MapFrom(dest => dest.Message))
                .ForMember(x => x.SenderId, opt => opt.MapFrom(dest => dest.Sender))
                .ForMember(x => x.ReceiverId, opt => opt.MapFrom(dest => dest.Receiver));

            #endregion

            #region Cart
            CreateMap<Cart, CartReponse>().ReverseMap();
            CreateMap<Cart, CartRequest>().ReverseMap();
            CreateMap<CartReponse, CartFilter>().ReverseMap();
            #endregion
        }
    }
}
