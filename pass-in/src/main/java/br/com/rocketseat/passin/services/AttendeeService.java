package br.com.rocketseat.passin.services;

import br.com.rocketseat.passin.domain.attendee.Attendee;
import br.com.rocketseat.passin.domain.attendee.exceptions.AttendeeAlreadyExistException;
import br.com.rocketseat.passin.domain.checkin.CheckIn;
import br.com.rocketseat.passin.dto.attendee.AttendeeDetails;
import br.com.rocketseat.passin.dto.attendee.AttendeesListResponseDTO;
import br.com.rocketseat.passin.repositories.AttendeeRepository;
import br.com.rocketseat.passin.repositories.CheckInRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class AttendeeService {

    private final AttendeeRepository attendeeRepository;
    private final CheckInRepository checkInRepository;

    public List<Attendee> getAllAttendeesFromEvent(String eventId){
        return this.attendeeRepository.findByEventId(eventId);
    }

    public AttendeesListResponseDTO getEventAttendees(String eventId){
        var attendeeList = this.getAllAttendeesFromEvent(eventId);
        var attendeeDetailsList = attendeeList.stream().map(attendee -> {
            var checkIn = this.checkInRepository.findByAttendeeId(attendee.getId());
            var checkedInAt = checkIn.<LocalDateTime>map(CheckIn::getCreatedAt).orElse(null);
            return new AttendeeDetails(attendee.getId(), attendee.getName(), attendee.getEmail(), attendee.getCreatedAt(), checkedInAt);
        }).toList();
        return new AttendeesListResponseDTO(attendeeDetailsList);
    }

    public void verifyAttendeeSubscription(String eventId, String email){
        Optional<Attendee> isAttendeeRegistered = this.attendeeRepository.findByEventIdAndEmail(eventId, email);
        if(isAttendeeRegistered.isPresent())
            throw new AttendeeAlreadyExistException("Attendee already registered");
    }

    public Attendee registerAttendee(Attendee newAttendee){
        this.attendeeRepository.save(newAttendee);
        return newAttendee;
    }
}
