using UnityEngine;
using TMPro;
using System.Collections.Generic;
using TapticPlugin;

public class QuestionHandler : MonoBehaviour
{
    private TransferManager transferManager;

    private List<string> conc_quest = new List<string>(); //List that concatenates all selected game modes
    private List<int> called_nums = new List<int>(); //Previously selected nums;

    private string[] classicParty = new string[136];
    private string[] awkward = new string[55];
    private string[] xxx = new string[123];
    private string[] heavydrinking = new string[74];
    private string[] ew = new string[34];
    private string[] university = new string[61];
    private string[] old = new string[40];
    private string[] karens = new string[40];
    private string[] chads = new string[40];

    private TMP_Text card_1_text, card_2_text, card_3_text;
    private TMP_Text card_1_num, card_2_num, card_3_num;

    private CardManager card_manager;

    private int q_num;
    private int q_reset_counter = 1;

    public int q_counter = 0;
    public int total_length;

    static System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {

        SetQuestions();

        transferManager = GameObject.FindGameObjectWithTag("TransferManager").GetComponent<TransferManager>();
        card_manager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();

        card_1_text = GameObject.FindGameObjectWithTag("Card_1_Text").GetComponent<TMP_Text>();
        card_2_text = GameObject.FindGameObjectWithTag("Card_2_Text").GetComponent<TMP_Text>();
        card_3_text = GameObject.FindGameObjectWithTag("Card_3_Text").GetComponent<TMP_Text>();

        card_1_num = GameObject.FindGameObjectWithTag("Card_1_Num").GetComponent<TMP_Text>();
        card_2_num = GameObject.FindGameObjectWithTag("Card_2_Num").GetComponent<TMP_Text>();
        card_3_num = GameObject.FindGameObjectWithTag("Card_3_Num").GetComponent<TMP_Text>();

        ConcatenateQuestions();
        FirstCardUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextCard()
    {
        TapticManager.Impact(ImpactFeedback.Medium);

        Debug.Log("Q_counter : " + q_counter);
        //q_counter != total_length
        if (conc_quest.Count != 0)
        {
            PickNum();
            UpdateLastCardNum();
            RemoveQuestion(q_num);
            Debug.Log("conc_quest size :" + conc_quest.Count);
            Debug.Log("q_num is : " + q_num);
        }
        else if (q_counter >= total_length && q_counter < total_length + 2)
        {
            q_counter++;
        }
        else
        {
            card_manager.LaunchModeScreen();
        }
    }

    void RemoveQuestion(int number)
    {
        conc_quest.Remove(conc_quest[number]);
    }

    void PickNum()
    {
        q_num = r.Next(conc_quest.Count-1);
        q_counter++;
    }

    void UpdateLastCardNum()
    {
        if (q_reset_counter == 3)
        {
            q_reset_counter = 1;
        }
        else
        {
            q_reset_counter++;
        }

        switch (q_reset_counter)
        {
            case 1:
                UpdateCardText(3);
                UpdateCardNumber(3);
                break;
            case 2:
                UpdateCardText(1);
                UpdateCardNumber(1);
                break;
            case 3:
                UpdateCardText(2);
                UpdateCardNumber(2);
                break;
        }
    }

    void UpdateCardText(int card)
    {
        switch (card)
        {
            case 1:
                card_1_text.text = conc_quest[q_num];
                break;
            case 2:
                card_2_text.text = conc_quest[q_num];
                break;
            case 3:
                card_3_text.text = conc_quest[q_num];
                break;
        }
    }

    void FirstCardUpdate()
    {
        PickNum();
        UpdateCardText(1);
        UpdateCardNumber(1);
        RemoveQuestion(q_num);

        PickNum();
        UpdateCardText(2);
        UpdateCardNumber(2);
        RemoveQuestion(q_num);

        PickNum();
        UpdateCardText(3);
        UpdateCardNumber(3);
        RemoveQuestion(q_num);
    }

    void UpdateCardNumber(int card)
    {
        switch (card)
        {
            case 1:
                card_1_num.text = q_counter + "/" + total_length;
                break;
            case 2:
                card_2_num.text = q_counter + "/" + total_length;
                break;
            case 3:
                card_3_num.text = q_counter + "/" + total_length;
                break;

        }
    }

    void ConcatenateQuestions()
    {
        foreach (int mode in transferManager.selected_packs)
        {
            switch (mode)
            {
                case 0:
                    conc_quest.AddRange(classicParty);
                    break;
                case 1:
                    conc_quest.AddRange(awkward);
                    break;
                case 2:
                    conc_quest.AddRange(xxx);
                    break;
                case 3:
                    conc_quest.AddRange(heavydrinking);
                    break;
                case 4:
                    conc_quest.AddRange(ew);
                    break;
                case 5:
                    conc_quest.AddRange(university);
                    break;
                case 6:
                    conc_quest.AddRange(old);
                    break;
                case 7:
                    conc_quest.AddRange(karens);
                    break;
                case 8:
                    conc_quest.AddRange(chads);
                    break;
            }
        }

        total_length = conc_quest.Count;
        Debug.Log("total length : " + total_length);
    }

    public void ClearQuestions()
    {
        conc_quest.Clear();
        transferManager.ClearSelectedPacks();
        q_reset_counter = 1;
        q_counter = 0;
        total_length = 0;
        q_num = 0;
    }

    void SetQuestions()
    {
        classicParty[0] = "…injured myself while trying to impress a girl or boy I was interested in.";
        classicParty[1] = "…ran away from cops.";
        classicParty[2] = "…accidentally sent someone to the hospital.";
        classicParty[3] = "…taken food out of a trash can and eaten it.";
        classicParty[4] = "…cried / flirted my way out of a speeding ticket.";
        classicParty[5] = "…taken part in a talent show.";
        classicParty[6] = "…made money by performing on the street.";
        classicParty[7] = "…broken something at a friend’s house and never told them.";
        classicParty[8] = "…snooped through a friend’s bathroom or bedroom without them knowing.";
        classicParty[9] = "…ruined someone else’s vacation.";
        classicParty[10] = "…walked for more than six hours.";
        classicParty[11] = "…jumped from a roof.";
        classicParty[12] = "…shoplifted.";
        classicParty[13] = "…seen an alligator or crocodile in the wild.";
        classicParty[14] = "…set my or someone else’s hair on fire (accidentally or not).";
        classicParty[15] = "…ridden an animal.";
        classicParty[16] = "…had a bad fall because I was walking and texting.";
        classicParty[17] = "…been arrested.";
        classicParty[18] = "…been a virgin/played fortnite. (double-drink if both)";
        classicParty[19] = "…gone surfing.";
        classicParty[20] = "…walked out of a movie because it was bad.";
        classicParty[21] = "…broken a bone.";
        classicParty[22] = "…tried to cut my own hair.";
        classicParty[23] = "…completely forgot my lines in a play.";
        classicParty[24] = "…shot a gun.";
        classicParty[25] = "…had a surprise party thrown for me.";
        classicParty[26] = "…cheated on a test.";
        classicParty[27] = "…dined and dashed.";
        classicParty[28] = "…gotten stitches.";
        classicParty[29] = "…fallen in love at first sight.";
        classicParty[30] = "…had a paranormal experience.";
        classicParty[31] = "…woken up and couldn’t move.";
        classicParty[32] = "…accidentally said “I love you” to someone.";
        classicParty[33] = "…hitchhiked.";
        classicParty[34] = "…been trapped in an elevator.";
        classicParty[35] = "…sung karaoke in front of people.";
        classicParty[36] = "…been on TV or the radio.";
        classicParty[37] = "…sent a message and then immediately regretted it.";
        classicParty[38] = "…been so sun burnt I couldn’t wear clothes.";
        classicParty[39] = "…had a crush on a friend’s parent.";
        classicParty[40] = "…been awake for two days straight.";
        classicParty[41] = "…thrown up on a roller coaster.";
        classicParty[42] = "…snuck into a movie.";
        classicParty[43] = "…been broken up with.";
        classicParty[44] = "…dyed my hair a crazy color.";
        classicParty[45] = "…had a physical fight with my best friend/sibling.";
        classicParty[46] = "…gotten slapped (wherever it may be).";
        classicParty[47] = "…worked with someone I hated with the burning passion of a thousand suns.";
        classicParty[48] = "…danced in an elevator.";
        classicParty[49] = "…cried in public because of a song.";
        classicParty[50] = "…texted for hours instead of calling the person.";
        classicParty[51] = "…chipped a tooth.";
        classicParty[52] = "…gone hunting.";
        classicParty[53] = "…had a tree house.";
        classicParty[54] = "…thrown something to a TV or computer screen.";
        classicParty[55] = "…been to a country in asia.";
        classicParty[56] = "…been screamed at by a customer at my job.";
        classicParty[57] = "…spent a night in the woods with no shelter.";
        classicParty[58] = "…read a whole novel in one day.";
        classicParty[59] = "…gone vegan.";
        classicParty[60] = "…been without heat for a winter or without A/C for a summer.";
        classicParty[61] = "…worn glasses without lenses.";
        classicParty[62] = "…gone scuba diving.";
        classicParty[63] = "…lied about a family member dying as an excuse to get out of doing something.";
        classicParty[64] = "…bungee jumped.";
        classicParty[65] = "…been to a country in Africa.";
        classicParty[66] = "…been on a fad diet.";
        classicParty[67] = "…been to a fashion show.";
        classicParty[68] = "…been electrocuted.";
        classicParty[69] = "…stolen something from a restaurant.";
        classicParty[70] = "…had a bad allergic reaction.";
        classicParty[71] = "…been in an embarrassing video that was uploaded to YouTube.";
        classicParty[72] = "…almost drowned.";
        classicParty[73] = "…worked at a fast food restaurant.";
        classicParty[74] = "…fainted.";
        classicParty[75] = "…looked through someone else’s phone without their permission.";
        classicParty[76] = "…been suspended.";
        classicParty[77] = "…drank coffee.";
        classicParty[78] = "…flashed my boobs at a concert/sports game.";
        classicParty[79] = "…gotten a tattoo.";
        classicParty[80] = "…cried after a movie.";
        classicParty[81] = "…lied on an application (to uni, on a cv...).";
        classicParty[82] = "…given money/food to a homeless person.";
        classicParty[83] = "…made a funny video and uploaded it to YouTube.";
        classicParty[84] = "…been denied entry to a club.";
        classicParty[85] = "…lost my contact lenses/glasses.";
        classicParty[86] = "…organised a suprise birthday party.";
        classicParty[87] = "…been bullied.";
        classicParty[88] = "…bullied someone.";
        classicParty[89] = "…told a racist joke.";
        classicParty[90] = "…freed the nipple (gone public without a bra).";
        classicParty[91] = "…worn UGG boots.";
        classicParty[92] = "…been to a church.";
        classicParty[93] = "…laughed so hard I peed.";
        classicParty[94] = "…smoked a cigar.";
        classicParty[95] = "…peed in a sink.";
        classicParty[96] = "…hurt an animal.";
        classicParty[97] = "…drawn a mustache on someone.";
        classicParty[98] = "…had streaks on snapchat";
        classicParty[99] = "…screamed at a bug.";
        classicParty[100] = "…been in a relationship with two people at a time.";
        classicParty[101] = "…eaten a big mac.";
        classicParty[102] = "…been on TV or the Radio.";
        classicParty[103] = "…given/received a fake number.";
        classicParty[104] = "…been to a wedding.";
        classicParty[105] = "…had a girlfriend/boyfriend.";
        classicParty[106] = "…talked my way out of a fine.";
        classicParty[107] = "…broken a bone.";
        classicParty[108] = "…had sex in this room.";
        classicParty[109] = "…vaped some phat clouds.";
        classicParty[110] = "…farted in a public space and blamed it on someone else.";
        classicParty[111] = "…seen my bestfriend naked.";
        classicParty[112] = "…went to a psychologist/therapist.";
        classicParty[113] = "…went paintballing.";
        classicParty[114] = "…seen \"2 girls 1 cup\".";
        classicParty[115] = "…been convinced that i'd seen a ghost/demon.";
        classicParty[116] = "…broken up with someone because i wanted to be with someone else.";
        classicParty[117] = "…driven an automatic car.";
        classicParty[118] = "…learned how to drive a manual car.";
        classicParty[119] = "…stolen money from my parents.";
        classicParty[120] = "…sung karaoke.";
        classicParty[121] = "…worn underwear from someone of the opposite sex.";
        classicParty[122] = "…listened to a song on repeat for more than an hour.";
        classicParty[123] = "…lied in this game. (everyone drinks).";
        classicParty[124] = "…peed in public.";
        classicParty[125] = "…been spanked in public.";
        classicParty[126] = "…been fooled on april's fools.";
        classicParty[127] = "…seen the titanic.";
        classicParty[128] = "…locked myself out of my house/room.";
        classicParty[129] = "…worn a thong (or tried one on).";
        classicParty[130] = "…used discount coupons.";
        classicParty[131] = "…smoked/vaped in the bathroom of a school.";
        classicParty[132] = "…cryed myself to sleep.";
        classicParty[133] = "…had surgery.";
        classicParty[134] = "…started college a virgin.";
        classicParty[135] = "…slept in a car.";

        heavydrinking[0] = "…bragged about something I have not done.";
        heavydrinking[1] = "…driven drunk.";
        heavydrinking[2] = "…experimented to figure out my sexual orientation.";
        heavydrinking[3] = "…said ‘i love you’ just to get laid.";
        heavydrinking[4] = "…spied on my neighbours.";
        heavydrinking[5] = "…doubted in my sexuality.";
        heavydrinking[6] = "…made fun of someone.";
        heavydrinking[7] = "…watched \"Keeping up with the Kardashians.\"";
        heavydrinking[8] = "…stole something with a higher value than $10.";
        heavydrinking[9] = "…really liked a song by Justin Bieber.";
        heavydrinking[10] = "…bet money on something.";
        heavydrinking[11] = "…done a handstand on one hand.";
        heavydrinking[12] = "…stalked an ex boyfriend or girlfriend on social media.";
        heavydrinking[13] = "…lied to a friend.";
        heavydrinking[14] = "…ditched class.";
        heavydrinking[15] = "…cheated on a test.";
        heavydrinking[16] = "…grabbed the wrong person’s hand.";
        heavydrinking[17] = "…fallen in love with someone I only met online.";
        heavydrinking[18] = "…lied in this game.";
        heavydrinking[19] = "…said “I love you” without feeling it.";
        heavydrinking[20] = "…been kicked out of a bar.";
        heavydrinking[21] = "…traveled by plane.";
        heavydrinking[22] = "…went skiing/snowboarding.";
        heavydrinking[23] = "…stuck gum under a desk.";
        heavydrinking[24] = "…bit my tongue.";
        heavydrinking[25] = "…taken nudes.";
        heavydrinking[26] = "…refused a kiss.";
        heavydrinking[27] = "…been unfaithful.";
        heavydrinking[28] = "…had a one night stand.";
        heavydrinking[29] = "…gone to the bathroom and then didn't wash my hands.";
        heavydrinking[30] = "…kissed someone of the same sex.";
        heavydrinking[31] = "…went skinny dipping.";
        heavydrinking[32] = "…woken up drunk after a night-out.";
        heavydrinking[33] = "…kissed my best friend.";
        heavydrinking[34] = "…taken drugs other than weed/alcohol.";
        heavydrinking[35] = "…smoked the mary-jane.";
        heavydrinking[36] = "…eaten food that fell on the floor.";
        heavydrinking[37] = "…been drunk playing this games.";
        heavydrinking[38] = "…kissed someone without knowing him/her.";
        heavydrinking[39] = "…been in the principal's office.";
        heavydrinking[40] = "…been with the former love of my best friend.";
        heavydrinking[41] = "…went topless at the beach.";
        heavydrinking[42] = "…had a friend with benefits.";
        heavydrinking[43] = "…fought in the street.";
        heavydrinking[44] = "…fallen asleep on the bus and passed my stop.";
        heavydrinking[45] = "…been in love with a teacher in high school/college.";
        heavydrinking[46] = "…been robbed.";
        heavydrinking[47] = "…sneaked into a party.";
        heavydrinking[48] = "…had a crush on my sister’s/brother's friend.";
        heavydrinking[49] = "…blacked-out from alcohol.";
        heavydrinking[50] = "…made out with the person holding this phone.";
        heavydrinking[51] = "…been friends with the person holding this phone.";
        heavydrinking[52] = "…failed my driving license test.";
        heavydrinking[53] = "…fallen from my bike.";
        heavydrinking[54] = "…tripped and fell in public.";
        heavydrinking[55] = "…spent too much money in the club on alcohol.";
        heavydrinking[56] = "…been kicked out of a bar/club.";
        heavydrinking[57] = "…taken way more sleeping aids than necessary.";
        heavydrinking[58] = "…ran through a red light.";
        heavydrinking[59] = "…made a snowman.";
        heavydrinking[60] = "…smashed an insect to pieces.";
        heavydrinking[61] = "…worn crocs.";
        heavydrinking[62] = "…pushed a pull door.";
        heavydrinking[63] = "…thrown up because someone else threw up.";
        heavydrinking[64] = "…set something on fire (people included).";
        heavydrinking[65] = "…changed a car tire.";
        heavydrinking[66] = "…had a blown up tire and couldn't figure out how to change it.";
        heavydrinking[67] = "…fallen asleep in a cinema.";
        heavydrinking[68] = "…tripped someone in public.";
        heavydrinking[69] = "…peed in the shower;";
        heavydrinking[70] = "…been on a double date.";
        heavydrinking[71] = "…been a dickhead.";
        heavydrinking[72] = "drunk-texted an ex.";
        heavydrinking[73] = "drunk-emailed a teacher.";

        xxx[0] = "…had sex in an open area (outside).";
        xxx[1] = "…had sex/foreplay in a car.";
        xxx[2] = "…been walked in on by my parents.";
        xxx[3] = "…had sex in a tent.";
        xxx[4] = "…had an STD scare.";
        xxx[5] = "…received/given anal.";
        xxx[6] = "…licked a bootyhole/had my bootyhole licked.";
        xxx[7] = "…sexted.";
        xxx[8] = "…had to take the morning after pill.";
        xxx[9] = "…had sex with more than one person at a time.";
        xxx[10] = "…had sex with someone out of pity.";
        xxx[11] = "…had sex on my period/with someone on their period.";
        xxx[12] = "…had a one night stand.";
        xxx[13] = "…fantasized about one of my teachers.";
        xxx[14] = "…kissed someone of the same sex.";
        xxx[15] = "…had to help a guy undo my bra/had the girl help me undo her bra.";
        xxx[16] = "…lied about my sexual performances to my peers.";
        xxx[17] = "…used lube.";
        xxx[18] = "…used objects to masturbate.";
        xxx[19] = "…licked a foot.";
        xxx[20] = "…flashed someone.";
        xxx[21] = "…had sex at a party.";
        xxx[22] = "…swallowed.";
        xxx[23] = "…masturbated.";
        xxx[24] = "…had semen on my face/came on someone’s face.";
        xxx[25] = "…had sex with a virgin.";
        xxx[26] = "…had an unsuccessful hook-up and ended up just sleeping next to the person.";
        xxx[27] = "…measured my penis/tried to shove a long object in my vajayjay to see how far up it would go.";
        xxx[28] = "…squirted/made her squirt";
        xxx[29] = "…done anything sexual in a public bathroom.";
        xxx[30] = "…done anything sexual in someone else’s house (neither mine or my partner’s).";
        xxx[31] = "…had sex more than once in a day.";
        xxx[32] = "…accidentally let one rip while having sex.";
        xxx[33] = "…got chocked/choked my partner during sex.";
        xxx[34] = "…asked to be spanked/spanked someone during sex.";
        xxx[35] = "…69’d.";
        xxx[36] = "…made out with someone while unconscious.";
        xxx[37] = "…read part of the kamasutra.";
        xxx[38] = "…peed in public.";
        xxx[39] = "…sat on someone’s face/got sat on the face.";
        xxx[40] = "…faked an orgasm.";
        xxx[41] = "…tied someone up/got tied up.";
        xxx[42] = "…regretted having sex with someone.";
        xxx[43] = "…almost puked while giving oral to someone.";
        xxx[44] = "…thought of someone else during sex.";
        xxx[45] = "…sucked a titty/got my titty sucked.";
        xxx[46] = "…foreplayed while at the movies.";
        xxx[47] = "…foreplayed while one of the two was driving.";
        xxx[48] = "…had sex with someone who’s name you didn’t know.";
        xxx[49] = "…sent a picture of my genitals to someone.";
        xxx[50] = "…moisturized my genitals.";
        xxx[51] = "…tried to lick/suck my own reproductive organ.";
        xxx[52] = "…got with someone more than 3 years older than me.";
        xxx[53] = "…had sex while my pet was around.";
        xxx[54] = "…bought something from a sex shop.";
        xxx[55] = "…dated more than one person at once.";
        xxx[56] = "…had a rebound.";
        xxx[57] = "…used tinder to get a date.";
        xxx[58] = "…been stood up by a tinder date.";
        xxx[59] = "…shaved my genitals.";
        xxx[60] = "…gone commando.";
        xxx[61] = "…cheated on my partner.";
        xxx[62] = "…been to a strip club.";
        xxx[63] = "…been accidentally pregnant/accidentally impregnated someone.";
        xxx[64] = "…had a crush on a family member.";
        xxx[65] = "…had a relationship i regretted afterwards.";
        xxx[66] = "…wanted to hook-up with someone playing this game.";
        xxx[67] = "…slept with someone within an hour of meeting them.";
        xxx[68] = "…worn cuffs during bedroom magic.";
        xxx[69] = "…worn latex clothing during the bam-bam in the ham.";
        xxx[70] = "…cosplayed during sexy-time.";
        xxx[71] = "…pretended to be kinky when you all you did was spank her ass 3 times and say \"you like dat\".";
        xxx[72] = "…seen \"2 girls 1 cup \".";
        xxx[73] = "…looked up \"blue waffle\" on google.";
        xxx[74] = "…bought tingly lube.";
        xxx[75] = "…bought a sextoy.";
        xxx[76] = "…fiddled with my noodle.";
        xxx[77] = "…jerked off to some anime titties.";
        xxx[78] = "…seen jennifer lawrence's nudes.";
        xxx[79] = "…had a premature ejaculation.";
        xxx[80] = "…had sex for less than a minute.";
        xxx[81] = "…been in contact with a tuna taco (smelly vagene).";
        xxx[82] = "…had a floppy.";
        xxx[83] = "…used the shower head to massage my clit/ass.";
        xxx[84] = "…let it grow wild down there.";
        xxx[85] = "…watched gay porn.";
        xxx[86] = "…watched transgender porn.";
        xxx[87] = "…done the college bingo.";
        xxx[88] = "…taken viagra.";
        xxx[89] = "…went on a sex-streak of more than 2 days, with a different person each day.";
        xxx[90] = "…went out with someone underage.";
        xxx[91] = "…lead someone on just for the kick of it.";
        xxx[92] = "…gone out with a psychopath.";
        xxx[93] = "…lied about myself to have sex with someone.";
        xxx[94] = "…had sex in the shower.";
        xxx[95] = "…had sex in a kitchen.";
        xxx[96] = "…been walked in on by a friend.";
        xxx[97] = "…recorded a sex-tape.";
        xxx[98] = "…seen kim k's sex tape.";
        xxx[99] = "…had a wet dream.";
        xxx[100] = "…touched an animal's private parts. (you sick fuck)";
        xxx[101] = "…used saliva as lube.";
        xxx[102] = "…had a threesome.";
        xxx[103] = "…tied up my partner before sex.";
        xxx[104] = "…talked about my fetishes with other people.";
        xxx[105] = "…paid for sex.";
        xxx[106] = "…gone to amsterdam's red light district.";
        xxx[107] = "…went to check out the prostitutes in my town.";
        xxx[108] = "…considered becoming a stripper.";
        xxx[109] = "…considered losing my virginity to a prostitute.";
        xxx[110] = "…lost my virginity to someone and regretted doing it with that person.";
        xxx[111] = "…had sex in a common toilet/bathroom";
        xxx[112] = "…lied about my sexual life to sound cool.";
        xxx[113] = "…had sex in a street.";
        xxx[114] = "…shaven my partner's pubic hair.";
        xxx[115] = "…foreplayed/had sex while some uninvolved people were in the same room.";
        xxx[116] = "…sucked a toe.";
        xxx[117] = "…been kicked out of a bar/club for fondling.";
        xxx[118] = "…shagged an ex.";
        xxx[119] = "…been upset with a partner for being awful in bed.";
        xxx[120] = "…slept with someone within an hour of meeting them.";
        xxx[121] = "…bought an OnlyFans subscription.";
        xxx[122] = "…considered signing up to OnlyFans.";

        university[0] = "…been to a lecture just to sign attendence and then left.";
        university[1] = "…skipped a lecture.";
        university[2] = "…had sex in a uni bathroom.";
        university[3] = "…failed an exam.";
        university[4] = "…had to skip meals in order to afford alcohol.";
        university[5] = "…done the college bingo.";
        university[6] = "…faked a friendship to get something out of it.";
        university[7] = "…dissed someone studying a stupid subject.";
        university[8] = "…seriously considered dropping out.";
        university[9] = "…been homesick.";
        university[10] = "…hated my roommate/flatmate.";
        university[11] = "…had daddy pay for premium student accomodation.";
        university[12] = "…been scared to take a massive shit in a shared bathroom.";
        university[13] = "…had an en-suite bathroom.";
        university[14] = "…passed out from alcohol.";
        university[15] = "…taken suspicious drugs that may not have been what i thought they were.";
        university[16] = "…smoked \"a weed\".";
        university[17] = "…wanted to roll a blunt but failed miserably.";
        university[18] = "…been a depressed engineering student.";
        university[19] = "…studied biochemistry.";
        university[20] = "…chosen my program because it looked easy.";
        university[21] = "…had less than 10h of lectures a week.";
        university[22] = "…submitted an assignment late.";
        university[23] = "…faked data for an experiment.";
        university[24] = "…forged signatures for consent forms/official documents.";
        university[25] = "…fucked someone so ugly it gave me nightmares.";
        university[26] = "…had a really bad hook up.";
        university[27] = "…fucked so many people that i cross path with one of them at least everyday.";
        university[28] = "…spent more time taking pictures before a night-out than I did at the night-out.";
        university[29] = "…been this person in group assignments that doesn't do anything.";
        university[30] = "…cheated on an exam.";
        university[31] = "…gotten drunk on my own to drown my sorrows.";
        university[32] = "…pretended i could dj to look cool.";
        university[33] = "…pretended i was smart but failed my exams.";
        university[34] = "…had high hopes for myself but ended up being the biggest disappointment in history.";
        university[35] = "…been a dickhead to someone.";
        university[36] = "…enjoyed university.";
        university[37] = "…enjoyed my course.";
        university[38] = "…peed in the streets on a night-out.";
        university[39] = "…thrown up because i drank too much.";
        university[40] = "…been sober on a night-out.";
        university[41] = "…tried studying but ended up falling asleep.";
        university[42] = "…went to a lecture still drunk.";
        university[43] = "…been heartbroken.";
        university[44] = "…pretended i could fuck anyone when my pull game is actually non-existent.";
        university[45] = "…started uni a virgin.";
        university[46] = "…lost my virginity on the first day of uni.";
        university[47] = "…considered stripping as a side hustle.";
        university[48] = "…considered prostitution to pay for my expensive lifestyle.";
        university[49] = "…been brutally rejected by someone i liked.";
        university[50] = "…almost single handedly finished a group assignment.";
        university[51] = "…been forced to diet because i didn't have enough money to last me the rest of the month.";
        university[52] = "…ate exclusively pizza or pasta for an entire week.";
        university[53] = "…dated someone just for their money.";
        university[54] = "…spent wayyy too much money on a date.";
        university[55] = "…stayed at home playing video-games instead of going out with my friends.";
        university[56] = "…been so loud during sex my house/flat mates heard everything.";
        university[57] = "…spent too much money on shots at the club.";
        university[58] = "…gave my credit card to my friends at the club.";
        university[59] = "…twerked in the club.";
        university[60] = "…showed up to graduation drunk/high.";

        ew[0] = "…eaten my booger.";
        ew[1] = "…put my finger in my ass and made someone smell it.";
        ew[2] = "…put my hand down there and asked someone to smell it.";
        ew[3] = "…not taken a shower for more than 2 days.";
        ew[4] = "…farted and blamed it on someone else.";
        ew[5] = "…farted so hard it wasn't only gas that came out of down there.";
        ew[6] = "…eaten ass.";
        ew[7] = "…eaten outdated food.";
        ew[8] = "…picked up a worm and eaten it.";
        ew[9] = "…sent a dick pic to a friend.";
        ew[10] = "…had a habit of chewing with my mouth open.";
        ew[11] = "…eaten raw eggs.";
        ew[12] = "…left skid marks in the toilet and didn't clean it.";
        ew[13] = "…shat in the wild.";
        ew[14] = "…made food and put the utensils back in the cupboard without cleaning them.";
        ew[15] = "…let mold grow on leftover food.";
        ew[16] = "…looked at my own butthole.";
        ew[17] = "…not washed my hair for more than 2 weeks.";
        ew[18] = "…drank budlight.";
        ew[19] = "…drunk coors light.";
        ew[20] = "…went to the toilet and didn't wash my hands.";
        ew[21] = "…thrown up in a public bathroom.";
        ew[22] = "…worn the same clothes for a week straight.";
        ew[23] = "…had an std.";
        ew[24] = "…bought a second-hand sextoy.";
        ew[25] = "…returned a sex toy after using it.";
        ew[26] = "…pooped myself.";
        ew[27] = "…slept on the streets.";
        ew[28] = "…had sex in public.";
        ew[29] = "…bought flavored lube and tasted it straight out of the bottle.";
        ew[30] = "…had soggy cereals and loved it.";
        ew[31] = "…stared at two people making out.";
        ew[32] = "…had sex while other people were in the room.";
        ew[33] = "…had a crush on my cousin.";

        awkward[0] = "…fantasized about someone in this room.";
        awkward[1] = "…made out with someone here.";
        awkward[2] = "…thought of having casual sex with someone in the room.";
        awkward[3] = "…tried but failed to get with someone in this room.";
        awkward[4] = "…wanted to shag more than one person in the room.";
        awkward[5] = "…masturbated to someone's photos at uni.";
        awkward[6] = "…awkwardly tried to get two people here to make out.";
        awkward[7] = "…had a crush on someone playing this game.";
        awkward[8] = "…thought someone playing this game was hot.";
        awkward[9] = "…wanted to get with someone playing this game but couldn't because our sexual orientations don't match.";
        awkward[10] = "…flirted with someone playing this game.";
        awkward[11] = "…stolen something from someone here.";
        awkward[12] = "…hated someone here with the burning passion of a thousand suns.";
        awkward[13] = "…thought of punching someone playing this game.";
        awkward[14] = "…thought that someone playing this game was a loser.";
        awkward[15] = "…been pissed off at someone playing this.";
        awkward[16] = "…got with someone playing this but never told anyone.";
        awkward[17] = "…left my friends on their birthday to avoid buying them a present.";
        awkward[18] = "…had dark secrets i've never told anyone in fear they'd never talk to me again.";
        awkward[19] = "…called my friend the wrong name.";
        awkward[20] = "…hated someone for their ethnicity.";
        awkward[21] = "…lied about my ethnicity to sound exotic.";
        awkward[22] = "…voted for trump.";
        awkward[23] = "…had a trump flag in my room.";
        awkward[24] = "…tried to make someone so drunk they'd go home with me.";
        awkward[25] = "…made out with someone so drunk neither of us remember.";
        awkward[26] = "…had a rock-hard boner in the presence of someone in the room.";
        awkward[27] = "…had a one-inch wonder willy.";
        awkward[28] = "…bragged about having a big dick but was disappointing in bed.";
        awkward[29] = "…came under a minute.";
        awkward[30] = "…not reached cloud 9 during sex.";
        awkward[31] = "…shaved my butt.";
        awkward[32] = "…cut myself while trying to shave it down there.";
        awkward[33] = "…slapped someone's ass in this room.";
        awkward[34] = "…twerked on someone in this room.";
        awkward[35] = "…made a cringey meme.";
        awkward[36] = "…had a YouTube channel.";
        awkward[37] = "…said i love you to someone here.";
        awkward[38] = "…peed in front of someone in this room.";
        awkward[39] = "…locked myself outside my room in my pajamas (pyjamas).";
        awkward[40] = "…seen someone in this room naked.";
        awkward[41] = "…been to a strip club but didn't spend any money.";
        awkward[42] = "…given a lap dance.";
        awkward[43] = "…been interrupted during sex by a phone call.";
        awkward[44] = "…gotten with multiple people of the same name.";
        awkward[45] = "…gotten with the crush of someone playing this game.";
        awkward[46] = "…made myself vomit after eating to stay fit.";
        awkward[47] = "…wished i'd been in an orgy.";
        awkward[48] = "…seriously used a cheesy pickup line on someone.";
        awkward[49] = "…flashed my tits to get something.";
        awkward[50] = "…had massive hickeys/lovebites.";
        awkward[51] = "…regretted a tattoo.";
        awkward[52] = "…had sex on kitchen table.";
        awkward[53] = "…had sex on a pool table.";
        awkward[54] = "…had sex in a closet.";

        old[0] = "…cried at work.";
        old[1] = "…had a crush on my boss.";
        old[2] = "…tried sexual advances on my boss to get a promotion.";
        old[3] = "…dated a coworker.";
        old[4] = "…hated someone i worked with.";
        old[5] = "…watched porn at work.";
        old[6] = "…browsed memes all day instead of working.";
        old[7] = "…lied about what i do for a living.";
        old[8] = "…stolen from my workplace.";
        old[9] = "…worked as a stripper.";
        old[10] = "…considered prostitution/stripping as a side hustle.";
        old[11] = "…been fired.";
        old[12] = "…worked in retail.";
        old[13] = "…worked at a fast food restaurant.";
        old[14] = "…worked in a fortune 500 company.";
        old[15] = "…worked as a freelancer.";
        old[16] = "…been unemployed.";
        old[17] = "…tried to become a famous youtuber.";
        old[18] = "…tried to become an instagram influencer.";
        old[19] = "…gotten kicked out of a club.";
        old[20] = "…came in less than one minute.";
        old[21] = "…lied about my ethnicity to sound exotic.";
        old[22] = "…voted for trump.";
        old[23] = "…had a trump flag in my room.";
        old[24] = "…tried to get someone here so drunk they'd get with me.";
        old[25] = "…gotten with someone so drunk neither of us remember.";
        old[26] = "…had a rock-hard boner in the presence of someone playing this game.";
        old[27] = "…had a one-inch wonder willy.";
        old[28] = "…bragged about having a big dick but was disappointing in bed.";
        old[29] = "…came under a minute.";
        old[30] = "…didn't reach cloud 9 during sex.";
        old[31] = "…shaved my butt.";
        old[32] = "…cut myself while trying to shave it down there.";
        old[33] = "…slapped someone's ass in this room.";
        old[34] = "…twerked on someone in this room.";
        old[35] = "…made a cringey meme.";
        old[36] = "…had a YouTube channel.";
        old[37] = "…said i love you to someone here.";
        old[38] = "…peed in front of someone in this room.";
        old[39] = "…taken viagra/cialis.";
    }
}