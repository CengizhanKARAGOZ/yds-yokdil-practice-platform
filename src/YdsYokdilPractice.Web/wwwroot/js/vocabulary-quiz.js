document.addEventListener("DOMContentLoaded", () => {
    const finishButton = document.getElementById("finishVocabularyQuizBtn");
    const resultBox = document.getElementById("vocabularyResultBox");

    if (!finishButton) return;

    finishButton.addEventListener("click", () => {
        const questions = document.querySelectorAll(".vocabulary-question");

        let correct = 0;
        let wrong = 0;
        let blank = 0;

        const wrongWords = [];

        questions.forEach(question => {
            const correctAnswer = question.dataset.correct;
            const selectedInput = question.querySelector("input[type='radio']:checked");
            const allInputs = question.querySelectorAll("input[type='radio']");

            if (!selectedInput) {
                blank++;
                question.classList.add("border-secondary");
            } else if (selectedInput.value === correctAnswer) {
                correct++;
                question.classList.add("border-success");
                selectedInput.closest(".vocabulary-option").classList.add("list-group-item-success");
            } else {
                wrong++;
                question.classList.add("border-danger");
                selectedInput.closest(".vocabulary-option").classList.add("list-group-item-danger");

                const correctInput = Array.from(allInputs).find(input => input.value === correctAnswer);

                if (correctInput) {
                    correctInput.closest(".vocabulary-option").classList.add("list-group-item-success");
                }

                const wordTitle = question.querySelector("h3")?.textContent?.trim();

                wrongWords.push({
                    word: wordTitle,
                    selected: selectedInput.value,
                    correct: correctAnswer,
                    solvedAt: new Date().toISOString()
                });
            }

            allInputs.forEach(input => {
                input.disabled = true;
            });

            const explanation = question.querySelector(".vocabulary-explanation");
            if (explanation) {
                explanation.classList.remove("d-none");
            }
        });

        document.getElementById("vocabCorrectCount").textContent = correct;
        document.getElementById("vocabWrongCount").textContent = wrong;
        document.getElementById("vocabBlankCount").textContent = blank;

        const previousWrongWords = JSON.parse(localStorage.getItem("wrong-vocabulary-words") || "[]");
        const updatedWrongWords = [...previousWrongWords, ...wrongWords];

        localStorage.setItem("wrong-vocabulary-words", JSON.stringify(updatedWrongWords));

        const result = {
            solvedAt: new Date().toISOString(),
            total: questions.length,
            correct,
            wrong,
            blank
        };

        localStorage.setItem("latest-vocabulary-quiz-result", JSON.stringify(result));

        resultBox.classList.remove("d-none");
        finishButton.disabled = true;
        finishButton.textContent = "Quiz Tamamlandı";

        resultBox.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
    });
});