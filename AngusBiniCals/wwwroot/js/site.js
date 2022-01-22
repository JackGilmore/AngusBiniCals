document.querySelectorAll(".btn-copy").forEach(item => {
    item.addEventListener("click",
        event => {
            var button = event.target;
            var fieldSelector = button.dataset.copyTarget;

            var field = document.querySelector(fieldSelector);

            field.select();
            field.setSelectionRange(0, 99999);

            document.execCommand("copy");

            if (button.dataset.copyChangeBtn === "true") {
                button.setAttribute("class", "btn btn-success btn-copy");
                button.innerText = "Copied!";
            }
        });
});