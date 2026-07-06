using Riok.Mapperly.Abstractions;

[assembly: MapperDefaults(
  EnumMappingStrategy = EnumMappingStrategy.ByName,
  PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseSensitive,
  ThrowOnMappingNullMismatch = true,
  ThrowOnPropertyMappingNullMismatch = true
)]
