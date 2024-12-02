package org.sid.ecommerce.Entities;

import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;

import lombok.*;

import java.util.List;

@Entity @NoArgsConstructor @AllArgsConstructor
@Data

public class Category {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    private String description;
    // Utiliser @JsonManagedReference pour la gestion de la référence dans la sérialisation
    @JsonManagedReference
    @OneToMany(mappedBy = "category")
    private List<Product> products;



}
