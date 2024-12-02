package org.sid.ecommerce.Repository;

import org.sid.ecommerce.Entities.Product;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ProductRepository extends JpaRepository<Product,Long> {

}
