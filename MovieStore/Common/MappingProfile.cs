using AutoMapper;
using MovieStore.Entities;

namespace MovieStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMovieViewModel, Movie>().ForMember(dest => dest.Actors, opt => opt.Ignore())
                                                   .ForMember(dest => dest.ReleaseDate, opt => opt.Ignore());
            CreateMap<CreateActorViewModel, Actor>();
            CreateMap<CreateDirectorViewModel, Director>();
            CreateMap<CreateUserViewModel, User>();
            CreateMap<Transaction, BasicTransactionViewModel>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Movie.Price + "$")).ReverseMap();
            CreateMap<Transaction, DetailedTransactionViewModel>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Movie.Price + "$"))
                .ReverseMap();
            CreateMap<PurchaseViewModel, Transaction>();
            CreateMap<User, BasicUserViewModel>().ForMember(dest => dest.Genres, opt => opt.Ignore());
            CreateMap<BasicUserViewModel, User>().ForMember(dest => dest.Genres, opt => opt.Ignore());
            //CreateMap<Movie, DetailedMovieModel>()
            //    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            //    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(x => x.Name + x.Surname)))
            //    .ForMember(dest=>dest.Director,opt=>opt.MapFrom(src=>src.Director.Name + src.Director.Surname));

            //CreateMap<Actor, BasicActorViewModel>();

            CreateMap<Movie, DetailedMovieModel>()
                                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname))
                                            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(a => new BasicActorViewModel { Actor = a.Name + " " + a.Surname })));

            CreateMap<Actor, DetailedActorViewModel>()
                                            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(a => new BasicMovieViewModel { Movie = a.Title + ", " + a.ReleaseDate.Year })));
            CreateMap<User, BasicUserViewModel>().ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(a => new BasicGenreModel { Name = a.Name })));

            CreateMap<Director, DirectorsViewModel>()
                                            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MoviesDirected.Select(a => new BasicMovieViewModel { Movie = a.Title + ", " + a.ReleaseDate.Year })));

            CreateMap<Movie, MoviesViewModel>()
                                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        }
    }
}
//.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(a => new Actor { Name = a.Name, Surname = a.Surname })))