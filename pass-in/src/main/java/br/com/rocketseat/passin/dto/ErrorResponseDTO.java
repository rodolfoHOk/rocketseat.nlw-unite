package br.com.rocketseat.passin.dto;

public record ErrorResponseDTO(
        Integer statusCode,
        String message
) {
}
