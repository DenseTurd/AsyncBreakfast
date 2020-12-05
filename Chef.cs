using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Chef : MonoBehaviour
{
    void Start()
    {
        Task breakfast = MakeBreakfast();
    }

    async Task MakeBreakfast()
    {
        Coffee cup = await PourCoffeeAsync();
        Debug.Log("Coffee ready");
        await Task.Delay(1000);

        var baconTask = FryBaconAsync(new Bacon[3] { new Bacon(), new Bacon(), new Bacon(), });
        await Task.Delay(8000);
        var toastTask = MakeToastWithButterAndJamAsync(new Bread[] { new Bread(), new Bread(), new Bread(), });
        await Task.Delay(4000);
        var eggsTask = FryEggsAsync(new Egg[2] { new Egg(), new Egg() });
        await Task.Delay(4000);

        var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
        while (breakfastTasks.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(breakfastTasks);
            if (finishedTask == eggsTask)
            {
                Debug.Log("Eggs on a plate");
            }
            if (finishedTask == baconTask)
            {
                Debug.Log("Bacon ready");
            }
            if (finishedTask == toastTask)
            {
                Debug.Log("Toast ready");
            }
            breakfastTasks.Remove(finishedTask);
        }

        await PourOjAsync();
        Debug.Log("Got juice!");
        Debug.Log("Breakfast ready!");
    }

    async Task<Coffee> PourCoffeeAsync()
    {
        Debug.Log("Making coffee");
        await Task.Delay(2500);
        return new Coffee();
    }

    async Task<Bacon[]> FryBaconAsync(Bacon[] bacons)
    {

        Debug.Log("Warming frying pan");
        await Task.Delay(4000);

        Debug.Log($"Putting {bacons.Length} rashers of bacon in tha pan");
        for (int baconNumber = 0; baconNumber < bacons.Length; baconNumber++)
        {
            await PutBaconInPanAsync(bacons[baconNumber], baconNumber);
        }

        Debug.Log("Cooking first side of bacon");
        await Task.Delay(10000);
        for (int rasher = 0; rasher < bacons.Length; rasher++)
        {
            await FlipBaconAsync(bacons[rasher], rasher);
        }
        Debug.Log("Cooking second side of bacon");
        await Task.Delay(10000);

        return bacons;
    }

    async Task PutBaconInPanAsync(Bacon bacon, int baconNumber)
    {
        Debug.Log($"Putting rasher {baconNumber + 1} in the pan");
        await Task.Delay(1000);
    }

    async Task FlipBaconAsync(Bacon bacon, int rasher)
    {
        Debug.Log($"Flipping rasher {rasher + 1}");
        await Task.Delay(1000);
    }

    async Task<Bread[]> MakeToastWithButterAndJamAsync(Bread[] breads)
    {
        var toasts = await ToastBreadAsync(breads);
        for (int slice = 0; slice < breads.Length; slice++)
        {
            await ButterToast(breads[slice], slice);
            await JamToast(breads[slice], slice);
        }
        return toasts;
    }

    async Task<Bread[]> ToastBreadAsync(Bread[] breads)
    {
        for (int slice = 0; slice < breads.Length; slice++)
        {
            Debug.Log($"Putting bread slice {slice + 1} in toaster");
            await Task.Delay(1000);
        }
        Debug.Log("Toasting");
        await Task.Delay(9000);

        foreach (var bread in breads)
        {
            bread.Toast = true;
        }
        Debug.Log("Toasted!");

        return breads;
    }

    async Task ButterToast(Bread toast, int slice)
    {
        Debug.Log($"Buttering toast slice {slice}");
        await Task.Delay(2000);
        toast.Buttered = true;
        Debug.Log($"toast slice {slice} buttery");
    }

    async Task JamToast(Bread toast, int slice)
    {
        Debug.Log($"Jamming toast slice {slice}");
        toast.Jammy = true;
        await Task.Delay(2000);
        Debug.Log($"toast slice {slice} Jammy");
    }

    async Task<Egg[]> FryEggsAsync(Egg[] eggs)
    {
        for (int eggNumber = 0; eggNumber < eggs.Length; eggNumber++)
        {
            await CrackEggAsync(eggs[eggNumber], eggNumber);
        }

        Debug.Log("Frying eggs");
        await Task.Delay(12000);

        return eggs;
    }

    async Task CrackEggAsync(Egg egg, int eggNumber)
    {
        Debug.Log($"Cracking egg {eggNumber + 1}");
        await Task.Delay(1000);
        egg.Cracked = true;
    }

    async Task<Juice> PourOjAsync()
    {
        Debug.Log("Pouring juice");
        await Task.Delay(3000);
        return new Juice();
    }
}
