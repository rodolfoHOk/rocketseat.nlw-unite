package br.com.rocketseat.passin.config;

import br.com.rocketseat.passin.domain.event.exceptions.EventNotFoundException;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@ControllerAdvice
public class ExceptionEntityHandler {

    @ExceptionHandler(EventNotFoundException.class)
    public ResponseEntity<Void> handleEventNotFound(EventNotFoundException exception){
        return ResponseEntity.notFound().build();
    }
}
