package br.com.rocketseat.passin.services;

import br.com.rocketseat.passin.dto.event.EventResponseDTO;
import br.com.rocketseat.passin.repositories.AttendeeRepository;
import br.com.rocketseat.passin.repositories.EventRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class EventService {

    private final EventRepository eventRepository;
    private final AttendeeRepository attendeeRepository;

    public EventResponseDTO getEventDetail(String eventId){
        var event = eventRepository.findById(eventId)
                .orElseThrow(() -> new RuntimeException(STR."Event not found with ID: \{eventId}"));
        var attendees = attendeeRepository.findByEventId(eventId);
        return new EventResponseDTO(event, attendees.size());
    }
}
