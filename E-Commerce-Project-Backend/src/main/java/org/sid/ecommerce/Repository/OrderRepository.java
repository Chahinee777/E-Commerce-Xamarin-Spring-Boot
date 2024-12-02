package org.sid.ecommerce.Repository;

import org.sid.ecommerce.Entities.Order;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OrderRepository extends JpaRepository<Order,Long>{
    
}
