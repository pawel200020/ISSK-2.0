using System.Globalization;
using Data.Configuration.Mappers;
using Data.DatabaseEnums;
using Microsoft.EntityFrameworkCore;
using ApplicationParameter = Configuration.Shared.ApplicationParameter;

namespace Data.Configuration;

internal class AppParameterRepository : IAppParameterRepository
{
    private ApplicationDbContext _dbContext;
    private readonly IAppParameterEnumMapper _mapper;

    public AppParameterRepository(ApplicationDbContext dbContext, IAppParameterEnumMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<string?> GetStringParameterValue(ApplicationParameter name)
        => await GetRequiredParameterTypeValue(name, ParameterTypeEnum.String);

    public async Task<int> GetIntParameterValue(ApplicationParameter name)
    {
        try
        {
            var dbValue = await GetRequiredParameterTypeValue(name, ParameterTypeEnum.Integer);
            return int.TryParse(dbValue, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result)
                ? result
                : throw new InvalidOperationException("current parameter is not devired type");
        }
        catch (InvalidOperationException ex ) when (ex.Message == "Sequence contains no matching element")
        {
            throw new InvalidOperationException("Parameter with given name and type does not exist in database");
        }   
    }

    public async Task<bool> GetBoolParameterValue(ApplicationParameter name)
    {
        var dbValue = await GetRequiredParameterTypeValue(name, ParameterTypeEnum.Boolean);
        return bool.TryParse(dbValue, out var result)
            ? result
            : throw new InvalidOperationException("current parameter is not devired type");
    }

    private async Task<string?> GetRequiredParameterTypeValue(ApplicationParameter name, ParameterTypeEnum type)
    {
        var nameAsString = Enum.GetName(_mapper.MapToDatabaseEnum(name));
        return (await _dbContext.ApplicationParameters.SingleAsync(p =>
            p.Name == nameAsString && p.ParameterType.Id == (int)type
        )).Value;
    }

    private async Task<bool> SetParameterValue(ApplicationParameter name, string value, ParameterTypeEnum type)
    {
        var nameAsString = Enum.GetName(_mapper.MapToDatabaseEnum(name));
        var parameter = await _dbContext.ApplicationParameters.SingleAsync(p =>
            p.Name == nameAsString && p.ParameterType.Id == (int)type);

        parameter.Value = value;
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetStringParameterValue(ApplicationParameter name, string value) =>
        await SetParameterValue(name, value, ParameterTypeEnum.String);

    public async Task<bool> SetIntParameterValue(ApplicationParameter name, int value) =>
        await SetParameterValue(name, value.ToString(), ParameterTypeEnum.Integer);

    public async Task<bool> SetBoolParameterValue(ApplicationParameter name, bool value) =>
        await SetParameterValue(name, value.ToString(), ParameterTypeEnum.Boolean);
}