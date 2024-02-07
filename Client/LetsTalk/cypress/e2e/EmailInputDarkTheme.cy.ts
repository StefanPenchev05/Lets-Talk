describe("Theme test", () => {
  it("toggles between dark and light theme", () => {
    cy.visit("http://localhost:5173/")

    cy.get('input[type:"email"]').type("test@example.com");

    cy.get('input[type:"email"]').should("have.class", "test@example.com");
  });
});
