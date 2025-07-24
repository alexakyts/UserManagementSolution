package com.usermanagement.test;

import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import static io.restassured.RestAssured.given;
import static org.hamcrest.Matchers.*;

public class UserApiTests {

    @BeforeAll
    public static void setup() {
        RestAssured.baseURI = "https://localhost";
        RestAssured.port = 7162;
        RestAssured.useRelaxedHTTPSValidation();
    }

    /**
     * Позитивний сценарій: Створення дійсного користувача.
     * Перевіряє, що API повертає статус 201 Created та коректні дані створеного користувача.
     */
    @Test
    public void testCreateValidUser() {
        String requestBody = "{\n" +
                "    \"fullName\": \"Mykhailo Kovalenko\",\n" +
                "    \"email\": \"mykhailo.k@example.com\",\n" +
                "    \"dateOfBirth\": \"1988-03-25T00:00:00\"\n" +
                "}";

        given()
                .contentType(ContentType.JSON)
                .body(requestBody)
                .when()
                .post("/users/createuser")
                .then()
                .assertThat()
                .statusCode(201)
                .body("id", notNullValue())
                .body("fullName", equalTo("Mykhailo Kovalenko"))
                .body("email", equalTo("mykhailo.k@example.com"));
    }

    /**
     * Негативний сценарій: Створення користувача з недійсною електронною поштою.
     * Перевіряє, що API повертає статус 400 Bad Request та відповідне повідомлення про помилку.
     */
    @Test
    public void testCreateUserWithInvalidEmail() {
        String requestBody = "{\n" +
                "    \"fullName\": \"Olena Test\",\n" +
                "    \"email\": \"invalid-email-format\",\n" + // Недійсна електронна пошта
                "    \"dateOfBirth\": \"1995-07-07T00:00:00\"\n" +
                "}";

        given()
                .contentType(ContentType.JSON)
                .body(requestBody)
                .when()
                .post("/users/createuser")
                .then()
                .assertThat()
                .statusCode(400)
                .body("message", containsString("Email is required and must be a valid email format."));
    }

    /**
     * Негативний сценарій: Створення користувача з майбутньою датою народження.
     * Перевіряє, що API повертає статус 400 Bad Request та відповідне повідомлення про помилку.
     */
    @Test
    public void testCreateUserWithFutureDateOfBirth() {
        String requestBody = "{\n" +
                "    \"fullName\": \"Future Person\",\n" +
                "    \"email\": \"future.person@example.com\",\n" +
                "    \"dateOfBirth\": \"2030-01-01T00:00:00\"\n" + // Дата народження в майбутньому
                "}";

        given()
                .contentType(ContentType.JSON)
                .body(requestBody)
                .when()
                .post("/users/createuser")
                .then()
                .assertThat()
                .statusCode(400)
                .body("message", containsString("DateOfBirth can`t be in the future."));
    }
}