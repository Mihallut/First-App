namespace First_App.Server.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(AutoMapper.Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
