using System.Reflection;

[assembly: AssemblyProduct ("Logo for Xamarin Workbooks")]
[assembly: AssemblyCopyright ("Copyright © Daniel Cazzulino 2016")]
[assembly: AssemblyMetadata ("owners", "kzu")]

[assembly: AssemblyVersion(ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch)]
[assembly: AssemblyFileVersion(ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch)]
[assembly: AssemblyInformationalVersion(ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch + "+" + ThisAssembly.Git.Commit)]

#if DEBUG
[assembly: AssemblyConfiguration ("DEBUG")]
#else
[assembly: AssemblyConfiguration ("RELEASE")]
#endif