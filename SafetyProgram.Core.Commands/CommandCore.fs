module SafetyProgram.Core.CommandCore

open SafetyProgram.Core

type command<'a> = {
    CanExecute : 'a -> bool
    Execute : 'a -> Option<'a->'a>
}

