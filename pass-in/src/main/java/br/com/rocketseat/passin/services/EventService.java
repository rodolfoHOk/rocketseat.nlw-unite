package br.com.rocketseat.passin.services;

import br.com.rocketseat.passin.domain.attendee.Attendee;
import br.com.rocketseat.passin.domain.event.Event;
import br.com.rocketseat.passin.domain.event.exceptions.EventFullException;
import br.com.rocketseat.passin.domain.event.exceptions.EventNotFoundException;
import br.com.rocketseat.passin.dto.attendee.AttendeeIdDTO;
import br.com.rocketseat.passin.dto.attendee.AttendeeRequestDTO;
import br.com.rocketseat.passin.dto.event.EventIdDTO;
import br.com.rocketseat.passin.dto.event.EventRequestDTO;
import br.com.rocketseat.passin.dto.event.EventResponseDTO;
import br.com.rocketseat.passin.repositories.EventRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.text.Normalizer;
import java.time.LocalDateTime;
import java.util.List;

@Service
@RequiredArgsConstructor
public class EventService {

    private final EventRepository eventRepository;
    private final AttendeeService attendeeService;

    public EventResponseDTO getEventDetail(String eventId){
        var event = this.getEventById(eventId);
        var attendees = this.attendeeService.getAllAttendeesFromEvent(eventId);
        return new EventResponseDTO(event, attendees.size());
    }

    public EventIdDTO createEvent(EventRequestDTO eventDTO){
        var newEvent = new Event();
        newEvent.setTitle(eventDTO.title());
        newEvent.setDetails(eventDTO.details());
        newEvent.setMaximumAttendees(eventDTO.maximumAttendees());
        newEvent.setSlug(this.createSlug(eventDTO.title()));
        this.eventRepository.save(newEvent);
        return new EventIdDTO(newEvent.getId());
    }

    public AttendeeIdDTO registerAttendeeOnEvent(String eventId, AttendeeRequestDTO attendeeRequestDTO){
        this.attendeeService.verifyAttendeeSubscription(eventId, attendeeRequestDTO.email());
        Event event = this.getEventById(eventId);
        List<Attendee> attendeeList = this.attendeeService.getAllAttendeesFromEvent(eventId);
        if(event.getMaximumAttendees() <= attendeeList.size())
            throw new EventFullException("Event is full");

        Attendee newAttendee = new Attendee();
        newAttendee.setName(attendeeRequestDTO.name());
        newAttendee.setEmail(attendeeRequestDTO.email());
        newAttendee.setEvent(event);
        newAttendee.setCreatedAt(LocalDateTime.now());
        var savedAttendee = this.attendeeService.registerAttendee(newAttendee);
        return new AttendeeIdDTO(savedAttendee.getId());
    }

    private Event getEventById(String eventId) {
        return this.eventRepository.findById(eventId)
                .orElseThrow(() -> new EventNotFoundException(STR."Event not found with ID: \{eventId}"));
    }

    private String createSlug(String text){
        String normalized = Normalizer.normalize(text, Normalizer.Form.NFD);
        return normalized
                .replaceAll("[\\p{InCOMBINING_DIACRITICAL_MARKS}]", "")
                .replaceAll("[^\\w\\s]", "")
                .replaceAll("\\s+", "-")
                .toLowerCase();
    }
}
