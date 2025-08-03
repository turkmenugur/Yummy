using AutoMapper;
using Yummy.WebApi.Dtos.AboutDtos;
using Yummy.WebApi.Dtos.CategoryDtos;
using Yummy.WebApi.Dtos.FeatureDtos;
using Yummy.WebApi.Dtos.ImageDtos;
using Yummy.WebApi.Dtos.MessageDtos;
using Yummy.WebApi.Dtos.NotificationDtos;
using Yummy.WebApi.Dtos.ProductDtos;
using Yummy.WebApi.Dtos.ReservationDtos;
using Yummy.WebApi.Entities;

namespace Yummy.WebApi.Mapping;

public class GeneralMapping : Profile
{
    //AutoMapper'da maplenecek ifadeler constructor içinde yazılıyor
    public GeneralMapping()
    {
        CreateMap<Feature, ResultFeatureDto>().ReverseMap();
        CreateMap<Feature, CreateFeatureDto>().ReverseMap();
        CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();
        
        CreateMap<Message, ResultMessageDto>().ReverseMap();
        CreateMap<Message, CreateMessageDto>().ReverseMap();
        CreateMap<Message, UpdateMessageDto>().ReverseMap();
        CreateMap<Message, GetByIdMessageDto>().ReverseMap();
        
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>()
            .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.CategoryName))
            .ReverseMap();
        
        CreateMap<Notification, ResultNotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        CreateMap<Notification, GetNotificationByIdDto>().ReverseMap();
        CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
        
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        
        CreateMap<About, ResultAboutDto>().ReverseMap();
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, GetAboutByIdDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        
        CreateMap<Reservation, ResultReservationDto>().ReverseMap();
        CreateMap<Reservation, CreateReservationDto>().ReverseMap();
        CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
        CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();

        CreateMap<Image, ResultImageDto>().ReverseMap();
        CreateMap<Image, CreateImageDto>().ReverseMap();
        CreateMap<Image, GetImageByIdDto>().ReverseMap();
        CreateMap<Image, UpdateImageDto>().ReverseMap();
    }
}