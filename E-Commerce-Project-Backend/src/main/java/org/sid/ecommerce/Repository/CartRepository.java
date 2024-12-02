package org.sid.ecommerce.Repository;

import org.sid.ecommerce.Entities.Cart;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface CartRepository extends JpaRepository<Cart, Long> {
    Cart findByUserIdAndProductId(Long idUser, Long idProduct);
    Integer countByUserId(Long idUser);
    List<Cart> findByUserId(Long idUser);
}

