package br.com.rocketseat.passin.controllers;

import br.com.rocketseat.passin.dto.attendee.AttendeesListResponseDTO;
import br.com.rocketseat.passin.dto.event.EventIdDTO;
import br.com.rocketseat.passin.dto.event.EventRequestDTO;
import br.com.rocketseat.passin.dto.event.EventResponseDTO;
import br.com.rocketseat.passin.services.AttendeeService;
import br.com.rocketseat.passin.services.EventService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.util.UriComponentsBuilder;

@RestController
@RequestMapping("/events")
@RequiredArgsConstructor
public class EventController {

    private final EventService eventService;
    private final AttendeeService attendeeService;

    @GetMapping("/{id}")
    public ResponseEntity<EventResponseDTO> getEvent(@PathVariable String id) {
        var event = this.eventService.getEventDetail(id);
        return ResponseEntity.ok(event);
    }

    @PostMapping
    public ResponseEntity<EventIdDTO> createEvent(@RequestBody EventRequestDTO body, UriComponentsBuilder uriComponentsBuilder){
        var eventIdDTO = this.eventService.createEvent(body);
        var uri = uriComponentsBuilder.path("/events/{id}").buildAndExpand(eventIdDTO.eventId()).toUri();
        return ResponseEntity.created(uri).body(eventIdDTO);
    }

    @GetMapping("/attendees/{id}")
    public ResponseEntity<AttendeesListResponseDTO> getEventAttendees(@PathVariable String id) {
        var attendeesListResponse = this.attendeeService.getEventAttendees(id);
        return ResponseEntity.ok(attendeesListResponse);
    }
}
