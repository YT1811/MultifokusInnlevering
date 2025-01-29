document.getElementById('conversionForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    // Clear previous results and errors
    document.getElementById('result').innerHTML = '';
    document.getElementById('error').innerHTML = '';

    const temperature = document.getElementById('temperature').value;
    const fromUnit = document.getElementById('fromUnit').value;
    const toUnit = document.getElementById('toUnit').value;

    try {
        const response = await fetch('http://localhost:5194/api/temperature/convert', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                value: parseFloat(temperature),
                fromUnit: fromUnit,
                toUnit: toUnit
            })
        });

        const data = await response.json();

        if (!response.ok) {
            throw new Error(data.error || 'Conversion failed');
        }

        const resultElement = document.getElementById('result');
        resultElement.innerHTML = `
            <h4 class="text-success">
                ${temperature}°${fromUnit} = ${data.result}°${toUnit}
            </h4>
        `;
    } catch (error) {
        document.getElementById('error').innerHTML = `
            <div class="alert alert-danger">
                ${error.message}
            </div>
        `;
        console.error('Error:', error);
    }
});