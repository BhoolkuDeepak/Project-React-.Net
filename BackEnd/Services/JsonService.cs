using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class JsonService
{
    // Define the file paths for storing the data.
    private readonly string _eventsFile = "Data/events.json";  // File for event data.
    private readonly string _attendanceFile = "Data/attendance.json";  // File for attendance data.
    private readonly string _membersFile = "Data/members.json";  // File for member data.

    // Event Operations
    public List<Event> GetEvents()
    {
        // This method reads the event data from the file and returns it as a list.
        return ReadFromFile<List<Event>>(_eventsFile);
    }

    public void AddEvent(Event newEvent)
    {
        // This method adds a new event to the list of events and then saves it to the file.
        var events = GetEvents();  // Get the current list of events from the file.
        newEvent.EventId = events.Count > 0 ? events.Max(e => e.EventId) + 1 : 1;  // Assign a new EventId.
        events.Add(newEvent);  // Add the new event to the list.
        SaveToFile(_eventsFile, events);  // Save the updated list of events to the file.
    }

    // Attendance Operations
    public List<Attendance> GetAttendances()
    {
        // This method reads the attendance data from the file and returns it as a list.
        return ReadFromFile<List<Attendance>>(_attendanceFile);
    }

    public void AddAttendance(Attendance attendance)
    {
        // This method adds a new attendance record to the list and then saves it to the file.
        var attendances = GetAttendances();  // Get the current list of attendances from the file.
        attendance.AttendanceId = attendances.Count > 0 ? attendances.Max(a => a.AttendanceId) + 1 : 1;  // Assign a new AttendanceId.
        attendances.Add(attendance);  // Add the new attendance to the list.
        SaveToFile(_attendanceFile, attendances);  // Save the updated list of attendances to the file.
    }

    // Member Operations
    public List<Member> GetMembers()
    {
        // This method reads the member data from the file and returns it as a list.
        return ReadFromFile<List<Member>>(_membersFile);
    }

    public void AddMember(Member member)
    {
        // This method adds a new member to the list and then saves it to the file.
        var members = GetMembers();  // Get the current list of members from the file.
        member.MemberId = members.Count > 0 ? members.Max(m => m.MemberId) + 1 : 1;  // Assign a new MemberId.
        members.Add(member);  // Add the new member to the list.
        SaveToFile(_membersFile, members);  // Save the updated list of members to the file.
    }

    // Helper method to read data from a JSON file and deserialize it into an object of type T.
    private T ReadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            // If the file doesn't exist, return a new instance of T (empty list for collections).
            return Activator.CreateInstance<T>();
        }
        var json = File.ReadAllText(filePath);  // Read the contents of the file.
        return JsonConvert.DeserializeObject<T>(json) ?? Activator.CreateInstance<T>();  // Deserialize the JSON to an object of type T.
    }

    // Helper method to serialize an object of type T to a JSON string and save it to a file.
    private void SaveToFile<T>(string filePath, T data)
    {
        // Serialize the data into a nicely formatted JSON string.
        var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);  // Write the JSON string to the file.
    }
}
