namespace FP.TimeTracking.Test.Service.Rules

open System
open Xunit
open FP.TimeTracking.Domain

module TimeTrackFutureRuleTests =
    open FP.TimeTracking.Service
    open FP.TimeTracking.Core

    let getTime _ = DateTime(2017, 01, 01)

    let getTimeTrackMock reportedDate =
        { Id = Guid.Empty; 
          IssueId = Guid.Empty;
          CreatedAt = DateTime.Now
          CreatedBy = String.Empty
          TimeMinutes = 0
          ReportedDate = reportedDate
          Description = String.Empty}

    [<Fact>]
    let ``Validate_ReportedDate_IsValid`` () =
        let mockedDateTime = getTime()
        let rule = TimeTrackFutureRule.Validate getTime
        let timeTrack = getTimeTrackMock (mockedDateTime.AddDays -1.0)

        let actual = rule(timeTrack);

        Assert.True(actual.IsValid)
        Assert.Equal(actual.ValidationResults.Length, 0)

    [<Fact>]
    let ``Validate_ReportedDate_IsGreaterThenCurrentDate`` () =

        let mockedDateTime = getTime()
        let rule = TimeTrackFutureRule.Validate getTime
        let timeTrack = getTimeTrackMock (mockedDateTime.AddDays 1.0)

        let actual = rule(timeTrack);

        Assert.False(actual.IsValid)
        Assert.Equal(actual.ValidationResults.Length, 1)
        Assert.Equal(actual.ValidationResults.Head.Level, LevelType.Warning)
        Assert.Equal(actual.ValidationResults.Head.Message, sprintf "Time track date (%s) can not be greater than current date (%s)." (timeTrack.ReportedDate.ToString()) (mockedDateTime.ToString()))
