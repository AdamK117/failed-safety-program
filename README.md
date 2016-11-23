# SafetyProgram

## Background

This code accompanys a [blog post](http://www.adamkewley.com/programming/2016/08/24/projects-that-break-you.html)

This is an archive of a **failed** C#/F#/WPF application prototype I tried to produce in my spare time in 2012/2013. Because this was my first version controlled project, only some commits will build - I wasn't experienced enough to keep a clean master branch.

I had been programming (mostly webapps) long before this project; however, this was one of my first *real* projects. Every programmer eventually experiences it: a project that's so above their experience level that they found themselves deep in the matrix.

For around 6 months, I spent all of my available spare time studying design patterns, application architecture, C#, F#, WPF, software deployment, and a bunch of other techniques. Many of the commits in this repository are me attempting to implement a new technique or architecture rather than deliver features. When you've got a new hammer, everything looks like a nail.

After this bout of coding, I was strung out. There's only so much grind-mode coding you can layer on top of a PhD, so I eventually had to make the decision to drop the project altogether and go 100 % with my research. A shame but, looking back on the codebase, the experience was worth it in-and-of itself - I find it *much* easier to roll out bigger ideas now.

## The Application

The UK government mandates that anyone working with hazardous chemicals performs a COSHH assesment. During my PhD, I had to do COSHH assesments *very* regularly. That became a problem because—as any synthetic chemist will likely testify—creating COSHH forms is a complete pain in the ass. It's also especially repetative because many chemists tend to use similar chemicals repeatably.

Most sane people would come up with a system of Word templates or pre-filled forms to get around this annoyance. I figured that wasn't good enough and tried to create an all-signing all-dancing application for making COSHH forms. This repo is an archive of some of the work I did.

The main features I wanted in this application were:

  - The ability for users to design their own safety documents using useful components (such as chemical tables, text boxes, etc.)
  - Chemical lookup from local storage, remote databases, chemical vendors, etc.
  - Documents to be saved to a centralized cloud so that safety managers can overlook all COSHH forms going through their institution
  - Extremely modular design so that new controls and IO classes can be added easily

Most of the commits (see log) are *actually* spent:

  - **Learning C#** - this was my first major clientside application. At the time, I found C# to be one of the most fun languages I've learnt, mostly because of Visual Studio (although I'm writing that statement in emacs).
  - **Learning deployment / version control** - My smaller projects got away with baby's first "name a file `_v2`" versioning. That method died out *very* quickly while I studied software development for this project
  - **Learning application architecture** - Because I was way out of my depth, I had to study books like Code Complete & Design Patterns just to implement the kind of features I was looking for.
