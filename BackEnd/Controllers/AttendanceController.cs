using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    // Dependency Injection
    private readonly JsonService _jsonService;

    // Constructor DI.
    public AttendanceController(JsonService jsonService)
    {
        _jsonService = jsonService;
    }

    // Event APIs
    [HttpPost("events")]
    public ActionResult CreateEvent([FromBody] Event newEvent)
    {
        _jsonService.AddEvent(newEvent);
        return CreatedAtAction(nameof(GetEvents), new { id = newEvent.EventId }, newEvent);
    }

    [HttpGet("events")]
    public ActionResult<IEnumerable<Event>> GetEvents()
    {
        return Ok(_jsonService.GetEvents());
    }

    // Member APIs
    [HttpPost("members")]
    public ActionResult CreateMember([FromBody] Member member)
    {
        _jsonService.AddMember(member);
        return CreatedAtAction(nameof(GetMembers), new { id = member.MemberId }, member);
    }

    [HttpGet("members")]
    public ActionResult<IEnumerable<Member>> GetMembers()
    {
        return Ok(_jsonService.GetMembers());
    } // <-- FIXED: Closing brace properly closes GetMembers()

    // Attendance APIs
    [HttpPost("attendance")]
    public ActionResult CreateAttendance([FromBody] Attendance attendance)
    {
        _jsonService.AddAttendance(attendance);
        return CreatedAtAction(nameof(GetAttendances), new { id = attendance.AttendanceId }, attendance);
    }

    [HttpGet("attendance")]
    public ActionResult<IEnumerable<Attendance>> GetAttendances()
    {
        return Ok(_jsonService.GetAttendances());
    }
}
