use JuniorDev;
with aggregationFunction as --wcześniej był błąd... 
(-- funkcja agregująca może sumować, zliczać itd..
-- poniżej zapisując select m.title sql nie wie co ma zrobić bo agregujemy kilka wartości w jedną komórkę
select r.movie_id, SUM(r.number_of_tickets) as sold_tickets 
from reservations r
inner join  movies m
on r.movie_id = m.id
group by movie_id
) select af.movie_id,m.title,af.sold_tickets from aggregationFunction af
inner join movies m -- left i inner join powinny zachować się tak samo w tym przypadku
on af.movie_id = m.id
order by af.sold_tickets DESC, movie_id ASC

