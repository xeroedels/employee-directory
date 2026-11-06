const deleteButtons = document.querySelectorAll(".delete-btn");

deleteButtons.forEach((button) => {
  button.addEventListener("click", async () => {
    const confirmed = confirm("Are you sure you want to delete this employee?");

    if (!confirmed) {
      return;
    }

    const id = button.dataset.id;

    try {
      const response = await fetch(`/api/employees/${id}`, {
        method: "DELETE",
      });

      if (response.ok) {
        button.closest("tr").remove();
        const totalRecords = document.getElementById("totalRecords");
        const currentTotal = Number(totalRecords.textContent);
        totalRecords.textContent = currentTotal - 1;
      } else {
        alert("Failed to delete employee.");
      }
    } catch {
      alert("Network error.");
    }
  });
});
