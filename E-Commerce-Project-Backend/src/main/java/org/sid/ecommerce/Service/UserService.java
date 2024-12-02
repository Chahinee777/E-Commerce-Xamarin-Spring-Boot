package org.sid.ecommerce.Service;

import org.sid.ecommerce.Entities.User;
import org.sid.ecommerce.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    public User registerUser(User user) {
        // Add any necessary validation or business logic here
        System.out.println("Saving user to the database: " + user.getEmail()); // Log the user email
        return userRepository.save(user);
    }

    public User login(String email, String password) {
        // Add login logic here
        return userRepository.findByEmail(email)
                .filter(user -> password.equals(user.getPassword()))
                .orElse(null);
    }

    public User getUserByEmail(String email) {
        return userRepository.findByEmail(email).orElse(null);
    }

    public void updatePassword(String email, String newPassword) {
        userRepository.updatePassword(email, newPassword);
    }

    public User getUserById(Long id) {
        return userRepository.findById(id).orElse(null);
    }
}