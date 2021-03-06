// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid",
        Scope = "namespace", Target = "ScoreoidUI")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Scope = "member",
        Target = "ScoreoidUI.DebugHelpers.#ToDebugString(System.Object,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid",
        Scope = "type", Target = "ScoreoidUI.ScoreoidManager")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid",
        Scope = "type", Target = "ScoreoidUI.ScoreoidCreatePlayerPage")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "ScoreoidUI.ScoreoidManager.#GetLeaderboard(System.String,System.String,System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "id", Scope = "member",
        Target = "ScoreoidUI.ScoreoidManager.#Initialize(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "key", Scope = "member",
        Target = "ScoreoidUI.ScoreoidManager.#Initialize(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member", Target = "ScoreoidUI.ScoreoidManager.#Initialize(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "member",
        Target = "ScoreoidUI.ScoreoidManager.#Initialize(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "password",
        Scope = "member", Target = "ScoreoidUI.ScoreoidManager.#password")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "username",
        Scope = "member", Target = "ScoreoidUI.ScoreoidManager.#username")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "username",
        Scope = "member", Target = "ScoreoidUI.ScoreoidManager.#username")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScoreClient",
        Scope = "member", Target = "ScoreoidUI.ScoreoidManager.#ScoreoidClient")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e", Scope = "member",
        Target =
            "ScoreoidUI.TextBoxHelpers.#OnWatermarkChanged(Windows.UI.Xaml.DependencyObject,Windows.UI.Xaml.DependencyPropertyChangedEventArgs)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid",
        Scope = "type", Target = "ScoreoidUI.ScoreoidOverlay")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "by", Scope = "member",
        Target = "ScoreoidUI.ScoreoidManager.#GetLeaderboard(System.String,System.String,System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member",
        Target = "ScoreoidUI.TextBoxHelpers.#WatermarkProperty")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "ScoreoidUI.Leaderboard")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scoreoid", Scope = "type", Target = "ScoreoidUI.ScoreoidPane")]
