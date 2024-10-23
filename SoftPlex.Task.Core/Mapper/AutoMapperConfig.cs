using AutoMapper;

namespace SoftPlex.Task.Core.Mapper;

public class AutoMapperConfig
{
    /// <summary>
    /// Mapper
    /// </summary>
    public static IMapper Mapper { get; private set; }

    /// <summary>
    /// Initialize mapper
    /// </summary>
    /// <param name="config">Mapper configuration</param>
    public static void Init(MapperConfiguration config)
    {
        Mapper = config.CreateMapper();
    }
}