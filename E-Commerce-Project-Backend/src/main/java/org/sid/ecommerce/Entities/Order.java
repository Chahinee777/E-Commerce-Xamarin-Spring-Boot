package org.sid.ecommerce.Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.Date;

@Entity
@Table(name = "`order`")
@AllArgsConstructor
@NoArgsConstructor
@Getter
@Setter
public class Order {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;
    private String CommandeId;
    private Date DateCommande;
    @ManyToOne
    private Product Product;
    private int prix;
    private int quantite;
    @ManyToOne
    private User User;
    private String Statue;
    private String typePayment;
    private String adresse;
}