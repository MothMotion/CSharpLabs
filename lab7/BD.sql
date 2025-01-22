--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

-- Started on 2025-01-21 21:54:32

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 239 (class 1255 OID 24585)
-- Name: getid(text, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getid(inp_table_name text, inp_column_name text) RETURNS integer
    LANGUAGE plpgsql
    AS $$
		DECLARE
			var_cur_max int4;
			var_temp_id int4;
		BEGIN
		
			SELECT cur_max INTO var_cur_max
			FROM spec
			WHERE table_name = inp_table_name AND column_name = inp_column_name;
		
			IF var_cur_max IS NOT NULL THEN
				var_cur_max := var_cur_max + 1;
				UPDATE spec
				SET cur_max = var_cur_max
				WHERE table_name = inp_table_name AND column_name = inp_column_name;
			ELSE
				EXECUTE format('SELECT COALESCE(MAX(%I), 0) FROM %I', inp_column_name, inp_table_name)
				INTO var_cur_max;
				var_cur_max := var_cur_max + 1;
			
				var_temp_id := public.getid('spec', 'id');
		
				INSERT INTO spec(id, table_name, column_name, cur_max)
				VALUES(var_temp_id, inp_table_name, inp_column_name, var_cur_max);

				-- Создание триггера.
				EXECUTE format('
				CREATE TRIGGER SpecTableUpdate_%s
				AFTER UPDATE ON %I
				REFERENCING NEW TABLE AS newTable
				FOR EACH STATEMENT
				EXECUTE FUNCTION updSpec(%I);

				CREATE TRIGGER SpecTableInsert_%s
				AFTER INSERT ON %I
				REFERENCING NEW TABLE AS newTable
				FOR EACH STATEMENT
				EXECUTE FUNCTION updSpec(%I);
				',
				--inp_table_name, inp_column_name,
				var_temp_id, inp_table_name, inp_column_name,
				var_temp_id, inp_table_name, inp_column_name);
			END IF;
		
			RETURN var_cur_max;
			
		END $$;


ALTER FUNCTION public.getid(inp_table_name text, inp_column_name text) OWNER TO postgres;

--
-- TOC entry 227 (class 1255 OID 16926)
-- Name: updspec(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.updspec() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
		DECLARE
			var_workplease int4;
		BEGIN
			EXECUTE format('SELECT COALESCE(MAX(%I),0) FROM newTable', TG_ARGV[0])
			INTO var_workplease;
		
			UPDATE spec SET cur_max = var_workplease
			WHERE table_name = TG_TABLE_NAME AND column_name = TG_ARGV[0] AND var_workplease > cur_max;
			
			RETURN NEW;
		END $$;


ALTER FUNCTION public.updspec() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 226 (class 1259 OID 49169)
-- Name: developer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.developer (
    id integer NOT NULL,
    name character varying NOT NULL,
    rating double precision,
    workers_amount smallint NOT NULL
);


ALTER TABLE public.developer OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 49168)
-- Name: developer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.developer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.developer_id_seq OWNER TO postgres;

--
-- TOC entry 4846 (class 0 OID 0)
-- Dependencies: 225
-- Name: developer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.developer_id_seq OWNED BY public.developer.id;


--
-- TOC entry 220 (class 1259 OID 41008)
-- Name: game; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.game (
    id integer NOT NULL,
    name character varying NOT NULL,
    release date DEFAULT CURRENT_DATE NOT NULL,
    series integer,
    cost money NOT NULL,
    developer_id integer NOT NULL,
    description text
);


ALTER TABLE public.game OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 41007)
-- Name: game_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.game_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.game_id_seq OWNER TO postgres;

--
-- TOC entry 4847 (class 0 OID 0)
-- Dependencies: 219
-- Name: game_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.game_id_seq OWNED BY public.game.id;


--
-- TOC entry 222 (class 1259 OID 41018)
-- Name: game_ownership; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.game_ownership (
    id integer NOT NULL,
    player_id integer NOT NULL,
    game_id integer NOT NULL,
    is_gift boolean NOT NULL,
    date timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


ALTER TABLE public.game_ownership OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 41017)
-- Name: game_ownership_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.game_ownership_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.game_ownership_id_seq OWNER TO postgres;

--
-- TOC entry 4848 (class 0 OID 0)
-- Dependencies: 221
-- Name: game_ownership_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.game_ownership_id_seq OWNED BY public.game_ownership.id;


--
-- TOC entry 224 (class 1259 OID 49153)
-- Name: game_series; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.game_series (
    id integer NOT NULL,
    name character varying NOT NULL
);


ALTER TABLE public.game_series OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 49152)
-- Name: game_series_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.game_series_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.game_series_id_seq OWNER TO postgres;

--
-- TOC entry 4849 (class 0 OID 0)
-- Dependencies: 223
-- Name: game_series_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.game_series_id_seq OWNED BY public.game_series.id;


--
-- TOC entry 218 (class 1259 OID 40998)
-- Name: player; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.player (
    id integer NOT NULL,
    nickname character varying NOT NULL,
    money_sum double precision DEFAULT 0 NOT NULL
);


ALTER TABLE public.player OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 40997)
-- Name: player_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.player_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.player_id_seq OWNER TO postgres;

--
-- TOC entry 4850 (class 0 OID 0)
-- Dependencies: 217
-- Name: player_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.player_id_seq OWNED BY public.player.id;


--
-- TOC entry 4670 (class 2604 OID 49172)
-- Name: developer id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.developer ALTER COLUMN id SET DEFAULT nextval('public.developer_id_seq'::regclass);


--
-- TOC entry 4665 (class 2604 OID 41011)
-- Name: game id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game ALTER COLUMN id SET DEFAULT nextval('public.game_id_seq'::regclass);


--
-- TOC entry 4667 (class 2604 OID 41021)
-- Name: game_ownership id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_ownership ALTER COLUMN id SET DEFAULT nextval('public.game_ownership_id_seq'::regclass);


--
-- TOC entry 4669 (class 2604 OID 49156)
-- Name: game_series id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_series ALTER COLUMN id SET DEFAULT nextval('public.game_series_id_seq'::regclass);


--
-- TOC entry 4663 (class 2604 OID 41001)
-- Name: player id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.player ALTER COLUMN id SET DEFAULT nextval('public.player_id_seq'::regclass);


--
-- TOC entry 4839 (class 0 OID 49169)
-- Dependencies: 226
-- Data for Name: developer; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.developer VALUES (1, 'Kanareika', 9.3, 10454);
INSERT INTO public.developer VALUES (2, 'Hexagon Phoenix', 9.2, 5077);
INSERT INTO public.developer VALUES (3, 'Black Square', 10, 0);
INSERT INTO public.developer VALUES (4, 'Phantom Games', 8.6, 65);
INSERT INTO public.developer VALUES (6, '1233', 121, 0);


--
-- TOC entry 4833 (class 0 OID 41008)
-- Dependencies: 220
-- Data for Name: game; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.game VALUES (10, 'Nier SemiAutomata', '2017-03-17', 2, '55,00', 2, 'После отключения Рунета россияне, не в силе противостоять РКН, бежали на Венеру. В течении 8 тысячилетии россияне предпринимали попытки обойти блокировку с помощью созданных Робонян.');
INSERT INTO public.game VALUES (13, 'Will For Speed', '2016-03-15', 3, '45,00', 4, 'Питерские разборки никогда ещё не достигали скорестей более 5 км/ч. Легендарный Главный Герой выезжает на солевые дорожки');
INSERT INTO public.game VALUES (11, 'Will For Speed: Least Wanted', '2005-11-15', 3, '30,00', 3, 'Главный герой прибывает в Нижний Тагил на модифицированной семёрке с целью стать первым среди последних. Однако в перед соревнованиями ему скрутили колёса и соскаблили обшивку, из-за чего Борис Бритва  победил заочно и отобрал семёрку.');
INSERT INTO public.game VALUES (24, '123', '2025-01-21', 8, '311,00', 6, '123');


--
-- TOC entry 4835 (class 0 OID 41018)
-- Dependencies: 222
-- Data for Name: game_ownership; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.game_ownership VALUES (20, 7, 10, false, '2025-01-21 17:38:12.195544');
INSERT INTO public.game_ownership VALUES (19, 7, 24, true, '2025-01-21 17:34:01.706277');


--
-- TOC entry 4837 (class 0 OID 49153)
-- Dependencies: 224
-- Data for Name: game_series; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.game_series VALUES (1, 'Metal Gear');
INSERT INTO public.game_series VALUES (2, 'Nier');
INSERT INTO public.game_series VALUES (3, 'Will For Speed');
INSERT INTO public.game_series VALUES (8, '21');


--
-- TOC entry 4831 (class 0 OID 40998)
-- Dependencies: 218
-- Data for Name: player; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.player VALUES (8, 'MegaCool Dude', 250);
INSERT INTO public.player VALUES (7, 'Cool Dude', 150);


--
-- TOC entry 4851 (class 0 OID 0)
-- Dependencies: 225
-- Name: developer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.developer_id_seq', 7, true);


--
-- TOC entry 4852 (class 0 OID 0)
-- Dependencies: 219
-- Name: game_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.game_id_seq', 24, true);


--
-- TOC entry 4853 (class 0 OID 0)
-- Dependencies: 221
-- Name: game_ownership_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.game_ownership_id_seq', 20, true);


--
-- TOC entry 4854 (class 0 OID 0)
-- Dependencies: 223
-- Name: game_series_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.game_series_id_seq', 8, true);


--
-- TOC entry 4855 (class 0 OID 0)
-- Dependencies: 217
-- Name: player_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.player_id_seq', 10, true);


--
-- TOC entry 4680 (class 2606 OID 49176)
-- Name: developer developer_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.developer
    ADD CONSTRAINT developer_pk PRIMARY KEY (id);


--
-- TOC entry 4676 (class 2606 OID 41023)
-- Name: game_ownership game_ownership_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_ownership
    ADD CONSTRAINT game_ownership_pk PRIMARY KEY (id);


--
-- TOC entry 4674 (class 2606 OID 41016)
-- Name: game game_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game
    ADD CONSTRAINT game_pk PRIMARY KEY (id);


--
-- TOC entry 4678 (class 2606 OID 49160)
-- Name: game_series game_series_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_series
    ADD CONSTRAINT game_series_pk PRIMARY KEY (id);


--
-- TOC entry 4672 (class 2606 OID 41006)
-- Name: player player_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.player
    ADD CONSTRAINT player_pk PRIMARY KEY (id);


--
-- TOC entry 4681 (class 2606 OID 49177)
-- Name: game game_developer_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game
    ADD CONSTRAINT game_developer_fk FOREIGN KEY (developer_id) REFERENCES public.developer(id) ON DELETE CASCADE;


--
-- TOC entry 4682 (class 2606 OID 49161)
-- Name: game game_game_series_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game
    ADD CONSTRAINT game_game_series_fk FOREIGN KEY (series) REFERENCES public.game_series(id) ON DELETE SET NULL;


--
-- TOC entry 4683 (class 2606 OID 41024)
-- Name: game_ownership game_ownership_game_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_ownership
    ADD CONSTRAINT game_ownership_game_fk FOREIGN KEY (game_id) REFERENCES public.game(id) ON DELETE CASCADE;


--
-- TOC entry 4684 (class 2606 OID 41029)
-- Name: game_ownership game_ownership_player_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.game_ownership
    ADD CONSTRAINT game_ownership_player_fk FOREIGN KEY (player_id) REFERENCES public.player(id) ON DELETE CASCADE;


-- Completed on 2025-01-21 21:54:33

--
-- PostgreSQL database dump complete
--

