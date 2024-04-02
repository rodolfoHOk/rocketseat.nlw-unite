package br.com.rocketseat.passin.repositories;

import br.com.rocketseat.passin.domain.checkin.CheckIn;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CheckInRepository extends JpaRepository<CheckIn, Integer> {

}
