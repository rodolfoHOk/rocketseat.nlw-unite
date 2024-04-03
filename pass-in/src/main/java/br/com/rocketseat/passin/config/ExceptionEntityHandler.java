package br.com.rocketseat.passin.config;

import br.com.rocketseat.passin.domain.attendee.exceptions.AttendeeAlreadyExistException;
import br.com.rocketseat.passin.domain.attendee.exceptions.AttendeeNotFoundException;
import br.com.rocketseat.passin.domain.checkin.exceptions.CheckInAlreadyExistsException;
import br.com.rocketseat.passin.domain.event.exceptions.EventFullException;
import br.com.rocketseat.passin.domain.event.exceptions.EventNotFoundException;
import br.com.rocketseat.passin.dto.ErrorResponseDTO;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@ControllerAdvice
public class ExceptionEntityHandler {

    @ExceptionHandler(EventNotFoundException.class)
    public ResponseEntity<ErrorResponseDTO> handleEventNotFound(EventNotFoundException exception){
        var errorResponse = new ErrorResponseDTO(HttpStatus.NOT_FOUND.value(), exception.getMessage());
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorResponse);
    }

    @ExceptionHandler(AttendeeAlreadyExistException.class)
    public ResponseEntity<ErrorResponseDTO> handleAttendeeAlreadyExist(AttendeeAlreadyExistException exception){
        var errorResponse = new ErrorResponseDTO(HttpStatus.CONFLICT.value(), exception.getMessage());
        return ResponseEntity.status(HttpStatus.CONFLICT).body(errorResponse);
    }

    @ExceptionHandler(EventFullException.class)
    public ResponseEntity<ErrorResponseDTO> handleEventFull(EventFullException exception){
        var errorResponse = new ErrorResponseDTO(HttpStatus.BAD_REQUEST.value(), exception.getMessage());
        return ResponseEntity.badRequest().body(errorResponse);
    }

    @ExceptionHandler(AttendeeNotFoundException.class)
    public ResponseEntity<ErrorResponseDTO> handleAttendeeFound(AttendeeNotFoundException exception){
        var errorResponse = new ErrorResponseDTO(HttpStatus.NOT_FOUND.value(), exception.getMessage());
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(errorResponse);
    }

    @ExceptionHandler(CheckInAlreadyExistsException.class)
    public ResponseEntity<ErrorResponseDTO> handleCheckInAlreadyExist(CheckInAlreadyExistsException exception){
        var errorResponse = new ErrorResponseDTO(HttpStatus.CONFLICT.value(), exception.getMessage());
        return ResponseEntity.status(HttpStatus.CONFLICT).body(errorResponse);
    }
}
