module SafetyProgram.Core.Models.Serialization.ConverterInterface

type TwoWayConverter<'a, 'b> = {
    ConvertTo : 'a->Option<'b>
    ConvertFrom : 'b->Option<'a>
}