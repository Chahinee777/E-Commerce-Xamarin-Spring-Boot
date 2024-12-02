package org.sid.ecommerce.Entities;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;

import lombok.*;

@Entity @NoArgsConstructor @AllArgsConstructor
@Data
public class Product {
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    private String description;

    private int price;
    private int quantity;

    private String imageName;
    private String imageType;
    @Lob
    private byte[] imageDate;


    // Utiliser @JsonBackReference pour ignorer cette référence dans la sérialisation
    @JsonBackReference
    @ManyToOne
    @JoinColumn(name = "category_id")
    private org.sid.ecommerce.Entities.Category category;
}
