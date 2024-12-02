package org.sid.ecommerce.Entities;

import jakarta.persistence.*;
import lombok.*;


@Entity @NoArgsConstructor @AllArgsConstructor
@Data

public class User  {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(unique = true)
    private String email;
    private String password;


}
