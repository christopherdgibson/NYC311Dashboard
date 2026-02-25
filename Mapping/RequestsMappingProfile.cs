namespace NYC311Dashboard.Mapping
{
    public class RequestsMappingProfile
    {
        public RequestsMappingProfile()
        {
            //CreateMap<RequestModel, RequestTableRow>()
            //     .ForMember(dest => dest.Borough, opt => opt.MapFrom(src => src.Borough.ToProperCase("en-GB")));
        }


        //public static Mapper InitializeAutomapper()
        //    {
        //        //Provide all the Mapping Configuration
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            //Configuring Employee and EmployeeDTO
        //            cfg.CreateMap<Employee, EmployeeDTO>()
        //            //Provide Mapping Configuration of FullName and Name Property
        //            .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))

        //            //Provide Mapping Dept of FullName and Department Property
        //            .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));
        //            //Any Other Mapping Configuration ....
        //        });
        //        //Create an Instance of Mapper and return that Instance
        //        var mapper = new Mapper(config);
        //        return mapper;
        //    }
    }
}
