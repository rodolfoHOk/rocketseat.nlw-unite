package br.com.rocketseat.passin.repositories;

import br.com.rocketseat.passin.domain.event.Event;
import org.springframework.data.jpa.repository.JpaRepository;

public interface EventRepository extends JpaRepository<Event, String> {

}
