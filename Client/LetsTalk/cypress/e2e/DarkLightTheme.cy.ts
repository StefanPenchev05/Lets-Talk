describe("DarkLightTheme", () => {
  let time;
  beforeEach(() => {
    cy.visit("http://localhost:5173/");
    cy.clock(new Date(2021, 5, 1, 19, 0, 0).getTime());
  });

  it('should have the correct colors in dark theme', () => {
    // Check the color of the email input label
    cy.get('[data-testid="email-input"] label')
      .should('have.css', 'color', 'rgb(255, 255, 255)'); // white

    // Check the color of the password input label
    cy.get('[for=":r3:"]')
      .should('have.css', 'color', 'rgb(255, 255, 255)'); // white

    // Check the color of the "Forgot Password?" link
    cy.get('p.underline')
      .should('have.css', 'color', 'rgb(139, 113, 221)'); // #8B71DD

    // Check the background color of the submit button
    cy.get('button[type="submit"]')
      .should('have.css', 'background-color', 'rgb(83, 65, 165)'); // #5341A5
  });
});
