function aplicarMascaraTelefone(selector) {
  const inputs = document.querySelectorAll(selector);

  inputs.forEach(function (input) {
    function formatPhoneNumber(value) {
      let digits = value.replace(/\D/g, "");
      if (digits.length > 11) digits = digits.slice(0, 11);

      let formatted = "";

      if (digits.length > 0) {
        formatted += "(" + digits.substring(0, 2);
      }
      if (digits.length >= 3) {
        formatted += ") " + digits.substring(2, digits.length >= 7 ? 7 : 6);
      }
      if (digits.length >= 7) {
        formatted += "-" + digits.substring(7, 11);
      }

      return formatted;
    }

    input.addEventListener("input", function () {
      const formatted = formatPhoneNumber(input.value);
      input.value = formatted;
    });

    // Remover a máscara antes de enviar o formulário
    const form = input.closest("form");
    if (form) {
      form.addEventListener("submit", function () {
        input.value = input.value.replace(/\D/g, "");
      });
    }
  });
}