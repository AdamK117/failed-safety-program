namespace SafetyProgram.Repositories

open System.IO

///<summary>Creates a new LocalFileRepository designed to read data (of type T)</summary>
///<param name="path">Local file system path to the repository</param>
///<exception cref="System.IO.FileNotFoundException">Thrown when path cannot be found</exception>
type public LocalFileRepository<'T>(path : string) = 
    //Private fields (let)

    //Constructor (do)
    do
        if File.Exists(path) = false then raise (System.IO.FileNotFoundException("Cannot find the file " + path))
            


    //Members
    
    member this.GetPath = 
        path
