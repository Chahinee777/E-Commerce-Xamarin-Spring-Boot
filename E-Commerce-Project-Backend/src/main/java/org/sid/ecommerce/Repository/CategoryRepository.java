package org.sid.ecommerce.Repository;

import org.sid.ecommerce.Entities.Category;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CategoryRepository extends JpaRepository<Category,Long> {


}
