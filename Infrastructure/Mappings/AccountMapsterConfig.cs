using ApplicationCore.Domain.Account;
using Infrastructure.Persistence.Entities.Account;
using Mapster;

namespace Infrastructure.Mappings;

public static class AccountMapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<AccountEntity, AccountModel>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Sub, src => src.Sub)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn)
            .Map(dest => dest.LastModifiedOn, src => src.LastModifiedOn)
            .Map(dest => dest.LastModifiedById, src => src.LastModifiedById);

        TypeAdapterConfig<AccountModel, AccountEntity>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Sub, src => src.Sub)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn)
            .Map(dest => dest.LastModifiedOn, src => src.LastModifiedOn)
            .Map(dest => dest.LastModifiedById, src => src.LastModifiedById)
            .Ignore(dest => dest.LastModifiedByAccount)
            .Ignore(dest => dest.DeletedOn);
    }
}