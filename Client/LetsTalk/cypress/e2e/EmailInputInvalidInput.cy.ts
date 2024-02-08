describe("Invalid Input", () => {
  it("typing invalid email", () => {
    cy.visit("http://localhost:5173/");

    cy.get('input[type="emailOrUsername"]').type("test@example");

    cy.get('input[type="emailOrUsername"]').then(($input) => {
      const describedById = $input.attr("aria-describedby");
      cy.get(`[id="${describedById}"]`).should(
        "have.text",
        "Please enter your username or email"
      );
    });

    cy.get('button[type="submit"]').click();

    cy.get('input[type="emailOrUsername"]').then(($input) => {
      const describedById = $input.attr("aria-describedby");
      cy.get(`[id="${describedById}"]`).should(
        "have.text",
        "Invalid email format"
      );
    });
  });

  it("typing invalid username", () => {
    cy.visit("http://localhost:5173/");

    cy.get('input[type="emailOrUsername"]').type("Stefan#2");

    cy.get('button[type="submit"]').click();

    cy.get('input[type="emailOrUsername"]').then(($input) => {
      const describedById = $input.attr("aria-describedby");
      cy.get(`[id="${describedById}"]`).should(
        "have.text",
        "Invalid username format"
      );
    });
  });
});
