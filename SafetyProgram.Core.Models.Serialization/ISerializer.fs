module SafetyProgram.Core.Models.Serialization.ConverterInterface

// Defines an interface for converting from one form to another
type IConverter<'a, 'b> =
    abstract member ConvertTo : 'a->Option<'b>
    abstract member ConvertFrom : 'b->Option<'a>