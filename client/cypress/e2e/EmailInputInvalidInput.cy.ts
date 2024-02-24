describe("Email Component", () => {
  beforeEach(() => {
    cy.visit("http://localhost:5173/");
  });

  it("should display the correct initial helper text", () => {
    cy.get("div").contains("Please enter your username or email").should("exist");
  });

  it("typing invalid email", () => {
    cy.get('input[type="emailOrUsername"]').type("test@example");

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
