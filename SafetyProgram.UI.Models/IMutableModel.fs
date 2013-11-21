namespace SafetyProgram.UI.Models

type IMutableModel<'a> =
    abstract ToImmutable : unit -> 'a
