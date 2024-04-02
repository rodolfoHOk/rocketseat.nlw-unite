package br.com.rocketseat.passin.repositories;

import br.com.rocketseat.passin.domain.attendee.Attendee;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AttendeeRepository extends JpaRepository<Attendee, String> {

}
