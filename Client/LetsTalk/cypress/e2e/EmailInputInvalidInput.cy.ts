describe("Invalid Input", () => {
  it("toggles between dark and light theme", () => {
    cy.visit("http://localhost:5173/")

    cy.get('input[type="email"]').type("test@example");

    cy.get('input[type="email"]').then(($input) => {
      const describedById = $input.attr('aria-describedby');
      cy.get(`[id="${describedById}"]`).should('have.text', 'Please enter your username or email');
    });

    cy.get('button[type="submit"]').click();
  });
});