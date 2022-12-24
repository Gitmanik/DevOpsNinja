function validateInputs() {
    var ret = true;

    const inputs = document.getElementsByTagName("input");

    for (let inp of inputs) {
        var pattern = inp.getAttribute("pattern");
        if (pattern == null)
            continue;

        const re = new RegExp(pattern);

        if (re.test(inp.value)) {
            inp.classList.add('is-valid')
            inp.classList.remove('is-invalid')
        }
        else {
            inp.classList.remove('is-valid')
            inp.classList.add('is-invalid')
            ret = false;
        }
    }

    return ret;
}