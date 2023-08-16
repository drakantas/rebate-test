--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3 (Debian 15.3-1.pgdg110+1)
-- Dumped by pg_dump version 15.3

-- Started on 2023-08-16 12:54:50 -05
CREATE TABLE public.products (
    id integer NOT NULL,
    identifier character varying NOT NULL,
    price numeric(16,4),
    uom character varying,
    supported_incentives integer
);

--
-- TOC entry 214 (class 1259 OID 16512)
-- Name: products_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.products ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 218 (class 1259 OID 16535)
-- Name: rebate_calculations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.rebate_calculations (
    id integer NOT NULL,
    rebate_identifier character varying NOT NULL,
    product_identifier character varying NOT NULL,
    amount numeric(16,4) NOT NULL,
    registered_at timestamp without time zone NOT NULL
);

--
-- TOC entry 217 (class 1259 OID 16534)
-- Name: rebate_history_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.rebate_calculations ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.rebate_history_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 216 (class 1259 OID 16520)
-- Name: rebates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.rebates (
    identifier character varying NOT NULL,
    incentive_type integer,
    amount numeric(16,4),
    percentage numeric(16,4)
);

--
-- TOC entry 3339 (class 0 OID 16513)
-- Dependencies: 215
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.products (identifier, price, uom, supported_incentives) OVERRIDING SYSTEM VALUE VALUES ('potato', 10.2560, 'kg', 7);


--
-- TOC entry 3342 (class 0 OID 16535)
-- Dependencies: 218
-- Data for Name: rebate_calculations; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.rebate_calculations (rebate_identifier, product_identifier, amount, registered_at) OVERRIDING SYSTEM VALUE VALUES ('potato_rebate', 'potato', 717.9200, '2023-08-16 17:37:55.011977');
INSERT INTO public.rebate_calculations (rebate_identifier, product_identifier, amount, registered_at) OVERRIDING SYSTEM VALUE VALUES ('potato_rebate_uom', 'potato', 512.8000, '2023-08-16 17:38:37.657736');
INSERT INTO public.rebate_calculations (rebate_identifier, product_identifier, amount, registered_at) OVERRIDING SYSTEM VALUE VALUES ('potato_rebate_uom', 'potato', 512.8000, '2023-08-16 17:40:13.98416');
INSERT INTO public.rebate_calculations (rebate_identifier, product_identifier, amount, registered_at) OVERRIDING SYSTEM VALUE VALUES ('potato_rebate_uom', 'potato', 100000.0000, '2023-08-16 17:45:52.014191');
INSERT INTO public.rebate_calculations (rebate_identifier, product_identifier, amount, registered_at) OVERRIDING SYSTEM VALUE VALUES ('potato_rebate', 'potato', 358.9600, '2023-08-16 17:46:30.184155');


--
-- TOC entry 3340 (class 0 OID 16520)
-- Dependencies: 216
-- Data for Name: rebates; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.rebates (identifier, incentive_type, amount, percentage) VALUES ('potato_rebate', 0, 500.6937, 0.7000);
INSERT INTO public.rebates (identifier, incentive_type, amount, percentage) VALUES ('potato_rebate_uom', 1, 1000.0000, 0.0000);


--
-- TOC entry 3349 (class 0 OID 0)
-- Dependencies: 214
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

--
-- TOC entry 3350 (class 0 OID 0)
-- Dependencies: 217
-- Name: rebate_history_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres

--
-- TOC entry 3189 (class 2606 OID 16519)
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (identifier);


--
-- TOC entry 3195 (class 2606 OID 16541)
-- Name: rebate_calculations rebate_history_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rebate_calculations
    ADD CONSTRAINT rebate_history_pkey PRIMARY KEY (id);


--
-- TOC entry 3193 (class 2606 OID 16526)
-- Name: rebates rebates_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rebates
    ADD CONSTRAINT rebates_pkey PRIMARY KEY (identifier);


--
-- TOC entry 3191 (class 2606 OID 16531)
-- Name: products uq_products_identifier; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT uq_products_identifier UNIQUE (identifier);



CREATE FUNCTION public.get_product(p_identifier character varying) RETURNS TABLE(id integer, identifier character varying, price numeric, uom character varying, supportedincentives integer)
    LANGUAGE sql
    AS $$
SELECT id as Id, identifier as Identifier, price as Price, uom as Uom, supported_incentives as SupportedIncentives FROM public.products
WHERE identifier = p_identifier;
$$;


ALTER FUNCTION public.get_product(p_identifier character varying) OWNER TO postgres;

--
-- TOC entry 221 (class 1255 OID 16547)
-- Name: get_rebate(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_rebate(p_identifier character varying) RETURNS TABLE(identifier character varying, incentive integer, amount numeric, percentage numeric)
    LANGUAGE sql
    AS $$
SELECT identifier as Identifier, incentive_type as Incentive, amount as Amount, percentage as Percentage FROM public.rebates
WHERE identifier = p_identifier;
$$;

--
-- TOC entry 220 (class 1255 OID 16542)
-- Name: save_rebate_calculation(character varying, character varying, numeric); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.save_rebate_calculation(IN p_product_identifier character varying, IN p_rebate_identifier character varying, IN p_amount numeric)
    LANGUAGE sql
    AS $$
INSERT INTO rebate_calculations (product_identifier, rebate_identifier, amount, registered_at)
VALUES (p_product_identifier, p_rebate_identifier, p_amount, NOW());

$$;


ALTER PROCEDURE public.save_rebate_calculation(IN p_product_identifier character varying, IN p_rebate_identifier character varying, IN p_amount numeric) OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16513)
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

-- Completed on 2023-08-16 12:54:50 -05

--
-- PostgreSQL database dump complete
--

